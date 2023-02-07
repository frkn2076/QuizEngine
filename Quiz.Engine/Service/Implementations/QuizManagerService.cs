using Mapster;
using Quiz.Engine.Data.Entities;
using Quiz.Engine.Data.Repositories.Contracts;
using Quiz.Engine.Helper;
using Quiz.Engine.Service.Contracts;
using Quiz.Engine.Service.DTOs;
using Quiz.Engine.ViewModels.Responses;

namespace Quiz.Engine.Service.Implementations;

public class QuizManagerService : IQuizManagerService
{
    private const int QUESION_POINT_DECIMAL_COUNT = 3;
    private readonly IQuizEngineRepository _quizEngineRepository;

    public QuizManagerService(IQuizEngineRepository quizEngineRepository)
    {
        _quizEngineRepository = quizEngineRepository;
    }

    public async Task<Guid> CreateQuizAsync(QuizDTO quizDTO, Guid userId)
    {
        var quizId = Guid.Empty;
        var quiz = quizDTO.Adapt<Data.Entities.Quiz>();
        quiz.UserId = userId;

        using (var connection = _quizEngineRepository.CreateDbConnection())
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                quizId = await _quizEngineRepository.CreateQuizAsync(quiz, connection, transaction);

                CheckGuidIsValid(quizId);

                foreach (var questionDTO in quizDTO.Questions)
                {
                    var question = questionDTO.Adapt<Question>();
                    question.QuizId = quizId;
                    var questionId = await _quizEngineRepository.CreateQuestionAsync(question, connection, transaction);

                    CheckGuidIsValid(questionId);

                    foreach (var answerDTO in questionDTO.Answers)
                    {
                        var answer = answerDTO.Adapt<Answer>();
                        answer.QuestionId = questionId;
                        var answerId = await _quizEngineRepository.CreateAsnwerAsync(answer, connection, transaction);

                        CheckGuidIsValid(answerId);
                    }
                }

                transaction.Commit();
            }
        }

        return quizId;
    }

    public async Task UpdateQuizAsync(QuizDTO quizDTO, Guid quizId, Guid userId)
    {
        var existingQuiz = await _quizEngineRepository.GetQuizAsync(quizId);

        if (existingQuiz is null)
        {
            throw new QuizRecordNotFoundException();
        }

        if (existingQuiz.UserId != userId)
        {
            throw new QuizBelongsToAnotherUserException();
        }

        if (existingQuiz.IsPublished)
        {
            throw new PublishedQuizCannotBeUpdatedException();
        }

        var quiz = quizDTO.Adapt<Data.Entities.Quiz>();
        quiz.Id = quizId;

        using (var connection = _quizEngineRepository.CreateDbConnection())
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                var isSuccessful = await _quizEngineRepository.UpdateQuizAsync(quiz, connection, transaction);

                if (!isSuccessful)
                {
                    throw new NoQuizFoundBelongsToUserException();
                }

                var questionIds = await _quizEngineRepository.DeleteQuestionAsync(quizId, connection, transaction);

                if (questionIds is not null && questionIds.Any())
                {
                    isSuccessful = await _quizEngineRepository.DeleteAnswerAsync(questionIds.ToList(), connection, transaction);

                    if (!isSuccessful)
                    {
                        throw new SomethingWentWrongException();
                    }
                }

                foreach (var questionDTO in quizDTO.Questions)
                {
                    var question = questionDTO.Adapt<Question>();
                    question.QuizId = quizId;
                    var questionId = await _quizEngineRepository.CreateQuestionAsync(question, connection, transaction);

                    CheckGuidIsValid(questionId);

                    foreach (var answerDTO in questionDTO.Answers)
                    {
                        var answer = answerDTO.Adapt<Answer>();
                        answer.QuestionId = questionId;

                        var answerId = await _quizEngineRepository.CreateAsnwerAsync(answer, connection, transaction);

                        CheckGuidIsValid(answerId);
                    }
                }

                transaction.Commit();
            }
        }
    }

    public async Task DeleteQuizAsync(Guid quizId, Guid userId)
    {
        using (var connection = _quizEngineRepository.CreateDbConnection())
        {
            connection.Open();

            var quiz = await _quizEngineRepository.GetQuizAsync(quizId, connection);

            if (quiz is null)
            {
                throw new QuizRecordNotFoundException();
            }

            if (quiz.UserId != userId)
            {
                throw new QuizBelongsToAnotherUserException();
            }

            if (!quiz.IsPublished)
            {
                throw new UnpublishedQuizCannotBeDeletedException();
            }

            var isSuccessful = await _quizEngineRepository.DeleteQuizAsync(quizId, connection);

            if (!isSuccessful)
            {
                throw new SomethingWentWrongException();
            }
        }
    }

    public async Task<IEnumerable<QuizSummaryDTO>> GetOtherUserQuizzesAsync(Guid userId)
    {
        var quizzes = await _quizEngineRepository.GetOtherUsersQuizzesAsync(userId);

        if (quizzes is null || !quizzes.Any())
        {
            throw new QuizRecordNotFoundException();
        }

        if (quizzes.Any(x => x.QuestionCount < 1))
        {
            throw new QuestionRecordNotFoundException();
        }

        var quizDTOs = quizzes.Adapt<QuizSummaryDTO[]>();

        return quizDTOs;
    }

    public async Task<QuizDTO> GetQuizAsync(Guid quizId, Guid userId)
    {
        var quizAll = await _quizEngineRepository.GetQuizAllAsync(quizId);

        if (quizAll is null || !quizAll.Any())
        {
            throw new QuizRecordNotFoundException();
        }

        var quiz = quizAll.First();

        if (quiz.UserId == userId)
        {
            throw new UsersCannotTakeThierOwnQuizException();
        }

        if (!quiz.IsPublished)
        {
            throw new QuizHasNotPublishedYetException();
        }

        var quizDTO = new QuizDTO()
        {
            Title = quiz.Title,
            Questions = quizAll.GroupBy(x => x.QuestionId).Select(x => new QuestionDTO()
            {
                Id = x.Key,
                Question = x.FirstOrDefault()?.Question ?? throw new QuestionRecordNotFoundException(),
                Answers = x.Select(y => new AnswerDTO()
                {
                    Id = y.AnswerId,
                    Answer = y.Answer,
                    IsCorrectAnswer = y.IsCorrectAnswer
                })
            })
        };

        return quizDTO;
    }

    public async Task<QuizExamResponseModel> SolveQuizAsync(QuizExamDTO quizExamDTO, Guid userId)
    {
        var response = new QuizExamResponseModel();

        var questionResponses = new List<QuestionExamResponseModel>();

        var quizPoint = 0.0;

        var existingSolution = await _quizEngineRepository.GetSolutionsAsync(userId);

        if (existingSolution is not null && existingSolution.Any())
        {
            throw new SameQuizCannotBeTakenMoreThanOnceException();
        }

        var quizDTO = await GetQuizAsync(quizExamDTO.Id, userId);

        using (var connection = _quizEngineRepository.CreateDbConnection())
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                foreach (var questionExamDTO in quizExamDTO.Questions)
                {
                    var questionDTOs = quizDTO.Questions.Where(x => x.Id == questionExamDTO.Id);

                    var questionSolutionCount = questionDTOs.Count();

                    if (questionSolutionCount > 1)
                    {
                        throw new SameQuestionCannotBeAnsweredMoreThanOnceException();
                    }

                    if (questionSolutionCount == 0)
                    {
                        throw new QuestionNotFoundInQuizException();
                    }

                    var questionDTO = questionDTOs.First();
                    var answerDTOs = questionDTO.Answers;

                    var rightAnswerSolutions = answerDTOs.Where(x => x.IsCorrectAnswer).Select(x => x.Id);
                    var wrongAnswerSolutions = answerDTOs.Where(x => !x.IsCorrectAnswer).Select(x => x.Id);

                    var weightForRightOptions = FormatHelper.CalculateWeight(rightAnswerSolutions.Count());
                    var weightForWrongOptions = FormatHelper.CalculateWeight(wrongAnswerSolutions.Count());

                    var questionPoint = 0.0;
                    var answerPoint = 0.0;

                    foreach (var answerId in questionExamDTO.Answers)
                    {
                        if (rightAnswerSolutions.Any(x => x.Equals(answerId)))
                        {
                            questionPoint += weightForRightOptions;
                            answerPoint = weightForRightOptions;
                        }
                        else if (wrongAnswerSolutions.Any(x => x.Equals(answerId)))
                        {
                            questionPoint -= weightForWrongOptions;
                            answerPoint = weightForWrongOptions * -1;
                        }

                        var solution = new Solution()
                        {
                            QuizId = quizExamDTO.Id,
                            QuestionId = questionExamDTO.Id,
                            AnswerId = answerId,
                            UserId = userId,
                            Point = answerPoint
                        };

                        var solutionId = await _quizEngineRepository.CreateSolutionAsync(solution, connection, transaction);

                        CheckGuidIsValid(solutionId);
                    }

                    if (questionPoint > 0.99)
                    {
                        questionPoint = 1;
                    }

                    var questionResponse = new QuestionExamResponseModel()
                    {
                        Question = questionDTO.Question,
                        Score = FormatHelper.FormatToDecimalPlaces(questionPoint, QUESION_POINT_DECIMAL_COUNT)
                    };

                    questionResponses.Add(questionResponse);

                    quizPoint += questionPoint;
                }

                transaction.Commit();
            }
        }

        response.TotalScore = FormatHelper.FormatToPercentage(quizPoint / quizDTO.Questions.Count());
        response.Questions = questionResponses;

        return response;
    }

    public async Task<IEnumerable<SolutionDetailDTO>> GetSolutionsOfUserAsync(Guid userId)
    {
        var solutionDetails = await _quizEngineRepository.GetSolutionDetailsAsync(userId);

        var solutionDetailDTOs = solutionDetails.GroupBy(x => x.QuizId).Select(x => new SolutionDetailDTO()
        {
            QuizTitle = x.FirstOrDefault()?.Title ?? throw new QuizTitleCannotBeFoundException(),
            Questions = x.GroupBy(y => y.QuestionId).Select(y => new SolutionQuestionDetailDTO()
            {
                Question = y.FirstOrDefault()?.Question ?? throw new QuizQuestionCannotBeFoundException(),
                Answers = y.Select(x => x.Answer),
                Point = FormatHelper.FormatToDecimalPlaces(y.Sum(z => z.Point), QUESION_POINT_DECIMAL_COUNT)
            })
        });

        return solutionDetailDTOs;
    }

    public async Task<QuizSolutionDTO> GetSolutionsOfQuizAsync(Guid quizId, Guid userId)
    {
        var solutionDetails = await _quizEngineRepository.GetSolutionDetailsOfQuizAsync(quizId, userId);

        if (solutionDetails is null || !solutionDetails.Any())
        {
            throw new SolutionNotFoundException();
        }

        var quizSolutionDTO = new QuizSolutionDTO();
        quizSolutionDTO.QuizTitle = solutionDetails?.First()?.Title ?? throw new QuizTitleCannotBeFoundException();
        quizSolutionDTO.UserSolutions = solutionDetails.GroupBy(x => x.UserId).Select(x => new QuizSolutionDetailDTO()
        {
            UserId = x.Key,
            Questions = x.GroupBy(y => y.QuestionId).Select(y => new QuizQuestionSolutionDetailDTO()
            {
                Question = y.FirstOrDefault()?.Question ?? throw new QuestionNotFoundInQuizException(),
                Answers = y.Select(z => z.Answer),
                Point = FormatHelper.FormatToDecimalPlaces(y.Sum(z => z.Point), QUESION_POINT_DECIMAL_COUNT)
            })
        });

        return quizSolutionDTO;
    }

    public async Task<IEnumerable<QuizSummaryDTO>> GetQuizzesOfUserAsync(Guid userId)
    {
        var quizzes = await _quizEngineRepository.GetQuizzesOfUserAsync(userId);

        if (quizzes is null || !quizzes.Any())
        {
            throw new QuizRecordNotFoundException();
        }

        if (quizzes.Any(x => x.QuestionCount < 1))
        {
            throw new QuestionRecordNotFoundException();
        }

        var quizDTOs = quizzes.Adapt<QuizSummaryDTO[]>();

        return quizDTOs;
    }

    #region Helper

    private void CheckGuidIsValid(Guid value)
    {
        if (value.Equals(Guid.Empty))
        {
            throw new SomethingWentWrongException();
        }
    }

    #endregion
}
