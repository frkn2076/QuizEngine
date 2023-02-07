using Dapper;
using Quiz.Engine.Data.Entities;
using Quiz.Engine.Data.Enums;
using Quiz.Engine.Data.Repositories.Contracts;
using Quiz.Engine.Helper;

namespace Quiz.Engine.Data;

public static class Feeder
{
    public static void CreateSchemas(IQuizEngineRepository repository)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var schemaFolderDirectory = Path.Combine(currentDirectory, Constants.SchemasFolderPath);
        var schemaFiles = Directory.GetFiles(schemaFolderDirectory);

        using (var connection = repository.CreateDbConnection())
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                foreach (var schemaFile in schemaFiles)
                {
                    var query = File.ReadAllText(schemaFile);
                    connection.Execute(query, transaction);
                }

                transaction.Commit();
            }
        }
    }

    public static void FillTemporaryData(IQuizEngineRepository repository)
    {
        using (var connection = repository.CreateDbConnection())
        {
            connection.Open();

            var user = connection.QueryFirstOrDefault<User>("SELECT * FROM public.user LIMIT 1");

            if (user is not null)
            {
                return;
            }

            using (var transaction = connection.BeginTransaction())
            {
                var user1 = ConstructUserEntity("frkn@gmail.com", "12345");
                var user2 = ConstructUserEntity("tuba@gmail.com", "12345");
                var user3 = ConstructUserEntity("rdvn@gmail.com", "12345");
                var user4 = ConstructUserEntity("batu@gmail.com", "12345");

                var userId1 = repository.CreateUserAsync(user1, connection, transaction).Result;
                var userId2 = repository.CreateUserAsync(user2, connection, transaction).Result;
                var userId3 = repository.CreateUserAsync(user3, connection, transaction).Result;
                var userId4 = repository.CreateUserAsync(user4, connection, transaction).Result;

                var quiz1 = ConstructQuizEntity(userId1, "Quiz-1", true);
                var quiz2 = ConstructQuizEntity(userId1, "Quiz-2", true);
                var quiz3 = ConstructQuizEntity(userId1, "Quiz-3", true);
                var quiz4 = ConstructQuizEntity(userId1, "Quiz-4", true);
                var quiz5 = ConstructQuizEntity(userId2, "Quiz-5", true);
                var quiz6 = ConstructQuizEntity(userId2, "Quiz-6", true);
                var quiz7 = ConstructQuizEntity(userId2, "Quiz-7", true);
                var quiz8 = ConstructQuizEntity(userId3, "Quiz-8", true);

                var quizId1 = repository.CreateQuizAsync(quiz1, connection, transaction).Result;
                var quizId2 = repository.CreateQuizAsync(quiz2, connection, transaction).Result;
                var quizId3 = repository.CreateQuizAsync(quiz3, connection, transaction).Result;
                var quizId4 = repository.CreateQuizAsync(quiz4, connection, transaction).Result;
                var quizId5 = repository.CreateQuizAsync(quiz5, connection, transaction).Result;
                var quizId6 = repository.CreateQuizAsync(quiz6, connection, transaction).Result;
                var quizId7 = repository.CreateQuizAsync(quiz7, connection, transaction).Result;
                var quizId8 = repository.CreateQuizAsync(quiz8, connection, transaction).Result;

                var question1 = ConstructQuestionEntity(quizId1, "Question-1", QuestionType.Single);
                var question2 = ConstructQuestionEntity(quizId2, "Question-2", QuestionType.Single);
                var question3 = ConstructQuestionEntity(quizId3, "Question-3", QuestionType.Single);
                var question4 = ConstructQuestionEntity(quizId4, "Question-4", QuestionType.Single);
                var question5 = ConstructQuestionEntity(quizId1, "Question-5", QuestionType.Single);
                var question6 = ConstructQuestionEntity(quizId1, "Question-6", QuestionType.Single);
                var question7 = ConstructQuestionEntity(quizId2, "Question-7", QuestionType.Single);
                var question8 = ConstructQuestionEntity(quizId1, "Question-8", QuestionType.Single);
                var question9 = ConstructQuestionEntity(quizId4, "Question-9", QuestionType.Single);
                var question10 = ConstructQuestionEntity(quizId1, "Question-10", QuestionType.Multiple);
                var question11 = ConstructQuestionEntity(quizId3, "Question-11", QuestionType.Multiple);
                var question12 = ConstructQuestionEntity(quizId1, "Question-12", QuestionType.Multiple);
                var question13 = ConstructQuestionEntity(quizId4, "Question-13", QuestionType.Multiple);
                var question14 = ConstructQuestionEntity(quizId1, "Question-14", QuestionType.Multiple);
                var question15 = ConstructQuestionEntity(quizId5, "Question-15", QuestionType.Multiple);
                var question16 = ConstructQuestionEntity(quizId7, "Question-16", QuestionType.Multiple);
                var question17 = ConstructQuestionEntity(quizId6, "Question-17", QuestionType.Multiple);
                var question18 = ConstructQuestionEntity(quizId7, "Question-18", QuestionType.Multiple);
                var question19 = ConstructQuestionEntity(quizId8, "Question-19", QuestionType.Multiple);
                var question20 = ConstructQuestionEntity(quizId1, "Question-20", QuestionType.Multiple);
                var question21 = ConstructQuestionEntity(quizId1, "Question-21", QuestionType.Multiple);
                var question22 = ConstructQuestionEntity(quizId5, "Question-22", QuestionType.Multiple);
                var question23 = ConstructQuestionEntity(quizId1, "Question-23", QuestionType.Multiple);
                var question24 = ConstructQuestionEntity(quizId8, "Question-24", QuestionType.Multiple);

                var questionId1 = repository.CreateQuestionAsync(question1, connection, transaction).Result;
                var questionId2 = repository.CreateQuestionAsync(question2, connection, transaction).Result;
                var questionId3 = repository.CreateQuestionAsync(question3, connection, transaction).Result;
                var questionId4 = repository.CreateQuestionAsync(question4, connection, transaction).Result;
                var questionId5 = repository.CreateQuestionAsync(question5, connection, transaction).Result;
                var questionId6 = repository.CreateQuestionAsync(question6, connection, transaction).Result;
                var questionId7 = repository.CreateQuestionAsync(question7, connection, transaction).Result;
                var questionId8 = repository.CreateQuestionAsync(question8, connection, transaction).Result;
                var questionId9 = repository.CreateQuestionAsync(question9, connection, transaction).Result;
                var questionId10 = repository.CreateQuestionAsync(question10, connection, transaction).Result;
                var questionId11 = repository.CreateQuestionAsync(question11, connection, transaction).Result;
                var questionId12 = repository.CreateQuestionAsync(question12, connection, transaction).Result;
                var questionId13 = repository.CreateQuestionAsync(question13, connection, transaction).Result;
                var questionId14 = repository.CreateQuestionAsync(question14, connection, transaction).Result;
                var questionId15 = repository.CreateQuestionAsync(question15, connection, transaction).Result;
                var questionId16 = repository.CreateQuestionAsync(question16, connection, transaction).Result;
                var questionId17 = repository.CreateQuestionAsync(question17, connection, transaction).Result;
                var questionId18 = repository.CreateQuestionAsync(question18, connection, transaction).Result;
                var questionId19 = repository.CreateQuestionAsync(question19, connection, transaction).Result;
                var questionId20 = repository.CreateQuestionAsync(question20, connection, transaction).Result;
                var questionId21 = repository.CreateQuestionAsync(question21, connection, transaction).Result;
                var questionId22 = repository.CreateQuestionAsync(question22, connection, transaction).Result;
                var questionId23 = repository.CreateQuestionAsync(question23, connection, transaction).Result;
                var questionId24 = repository.CreateQuestionAsync(question24, connection, transaction).Result;

                var answer1 = ConstructAnswerEntity(questionId1, "Answer-1", true);
                var answer2 = ConstructAnswerEntity(questionId1, "Answer-2", false);
                var answer3 = ConstructAnswerEntity(questionId1, "Answer-3", false);
                var answer4 = ConstructAnswerEntity(questionId1, "Answer-4", false);
                var answer5 = ConstructAnswerEntity(questionId1, "Answer-5", false);
                var answer6 = ConstructAnswerEntity(questionId2, "Answer-6", true);
                var answer7 = ConstructAnswerEntity(questionId2, "Answer-7", false);
                var answer8 = ConstructAnswerEntity(questionId3, "Answer-8", false);
                var answer9 = ConstructAnswerEntity(questionId3, "Answer-9", true);
                var answer10 = ConstructAnswerEntity(questionId4, "Answer-10", true);
                var answer11 = ConstructAnswerEntity(questionId4, "Answer-11", false);
                var answer12 = ConstructAnswerEntity(questionId5, "Answer-12", false);
                var answer13 = ConstructAnswerEntity(questionId5, "Answer-13", false);
                var answer14 = ConstructAnswerEntity(questionId5, "Answer-14", true);
                var answer15 = ConstructAnswerEntity(questionId5, "Answer-15", false);
                var answer16 = ConstructAnswerEntity(questionId6, "Answer-16", false);
                var answer17 = ConstructAnswerEntity(questionId6, "Answer-17", true);
                var answer18 = ConstructAnswerEntity(questionId7, "Answer-18", false);
                var answer19 = ConstructAnswerEntity(questionId7, "Answer-19", true);
                var answer20 = ConstructAnswerEntity(questionId7, "Answer-20", false);
                var answer21 = ConstructAnswerEntity(questionId8, "Answer-21", true);
                var answer22 = ConstructAnswerEntity(questionId8, "Answer-22", false);
                var answer23 = ConstructAnswerEntity(questionId9, "Answer-23", true);
                var answer24 = ConstructAnswerEntity(questionId9, "Answer-24", false);
                var answer25 = ConstructAnswerEntity(questionId10, "Answer-25", true);
                var answer26 = ConstructAnswerEntity(questionId10, "Answer-26", true);
                var answer27 = ConstructAnswerEntity(questionId10, "Answer-27", false);
                var answer28 = ConstructAnswerEntity(questionId11, "Answer-28", true);
                var answer29 = ConstructAnswerEntity(questionId11, "Answer-29", true);
                var answer30 = ConstructAnswerEntity(questionId12, "Answer-30", false);
                var answer31 = ConstructAnswerEntity(questionId12, "Answer-31", true);
                var answer32 = ConstructAnswerEntity(questionId12, "Answer-32", false);
                var answer33 = ConstructAnswerEntity(questionId12, "Answer-33", true);
                var answer34 = ConstructAnswerEntity(questionId13, "Answer-34", false);
                var answer35 = ConstructAnswerEntity(questionId13, "Answer-35", true);
                var answer36 = ConstructAnswerEntity(questionId13, "Answer-36", true);
                var answer37 = ConstructAnswerEntity(questionId14, "Answer-37", true);
                var answer38 = ConstructAnswerEntity(questionId14, "Answer-38", true);
                var answer39 = ConstructAnswerEntity(questionId14, "Answer-39", true);
                var answer40 = ConstructAnswerEntity(questionId15, "Answer-40", true);
                var answer41 = ConstructAnswerEntity(questionId15, "Answer-41", true);
                var answer42 = ConstructAnswerEntity(questionId16, "Answer-42", true);
                var answer43 = ConstructAnswerEntity(questionId16, "Answer-43", true);
                var answer44 = ConstructAnswerEntity(questionId17, "Answer-44", true);
                var answer45 = ConstructAnswerEntity(questionId17, "Answer-45", true);
                var answer46 = ConstructAnswerEntity(questionId18, "Answer-46", false);
                var answer47 = ConstructAnswerEntity(questionId18, "Answer-47", false);
                var answer48 = ConstructAnswerEntity(questionId18, "Answer-48", true);
                var answer49 = ConstructAnswerEntity(questionId19, "Answer-49", false);
                var answer50 = ConstructAnswerEntity(questionId19, "Answer-50", false);
                var answer51 = ConstructAnswerEntity(questionId20, "Answer-51", false);
                var answer52 = ConstructAnswerEntity(questionId20, "Answer-52", false);
                var answer53 = ConstructAnswerEntity(questionId21, "Answer-53", true);
                var answer54 = ConstructAnswerEntity(questionId21, "Answer-54", true);
                var answer55 = ConstructAnswerEntity(questionId22, "Answer-55", true);
                var answer56 = ConstructAnswerEntity(questionId22, "Answer-56", true);
                var answer57 = ConstructAnswerEntity(questionId23, "Answer-57", true);
                var answer58 = ConstructAnswerEntity(questionId23, "Answer-58", false);
                var answer59 = ConstructAnswerEntity(questionId24, "Answer-59", false);
                var answer60 = ConstructAnswerEntity(questionId24, "Answer-60", false);

                var answerId1 = repository.CreateAsnwerAsync(answer1, connection, transaction).Result;
                var answerId2 = repository.CreateAsnwerAsync(answer2, connection, transaction).Result;
                var answerId3 = repository.CreateAsnwerAsync(answer3, connection, transaction).Result;
                var answerId4 = repository.CreateAsnwerAsync(answer4, connection, transaction).Result;
                var answerId5 = repository.CreateAsnwerAsync(answer5, connection, transaction).Result;
                var answerId6 = repository.CreateAsnwerAsync(answer6, connection, transaction).Result;
                var answerId7 = repository.CreateAsnwerAsync(answer7, connection, transaction).Result;
                var answerId8 = repository.CreateAsnwerAsync(answer8, connection, transaction).Result;
                var answerId9 = repository.CreateAsnwerAsync(answer9, connection, transaction).Result;
                var answerId10 = repository.CreateAsnwerAsync(answer10, connection, transaction).Result;
                var answerId11 = repository.CreateAsnwerAsync(answer11, connection, transaction).Result;
                var answerId12 = repository.CreateAsnwerAsync(answer12, connection, transaction).Result;
                var answerId13 = repository.CreateAsnwerAsync(answer13, connection, transaction).Result;
                var answerId14 = repository.CreateAsnwerAsync(answer14, connection, transaction).Result;
                var answerId15 = repository.CreateAsnwerAsync(answer15, connection, transaction).Result;
                var answerId16 = repository.CreateAsnwerAsync(answer16, connection, transaction).Result;
                var answerId17 = repository.CreateAsnwerAsync(answer17, connection, transaction).Result;
                var answerId18 = repository.CreateAsnwerAsync(answer18, connection, transaction).Result;
                var answerId19 = repository.CreateAsnwerAsync(answer19, connection, transaction).Result;
                var answerId20 = repository.CreateAsnwerAsync(answer20, connection, transaction).Result;
                var answerId21 = repository.CreateAsnwerAsync(answer21, connection, transaction).Result;
                var answerId22 = repository.CreateAsnwerAsync(answer22, connection, transaction).Result;
                var answerId23 = repository.CreateAsnwerAsync(answer23, connection, transaction).Result;
                var answerId24 = repository.CreateAsnwerAsync(answer24, connection, transaction).Result;
                var answerId25 = repository.CreateAsnwerAsync(answer25, connection, transaction).Result;
                var answerId26 = repository.CreateAsnwerAsync(answer26, connection, transaction).Result;
                var answerId27 = repository.CreateAsnwerAsync(answer27, connection, transaction).Result;
                var answerId28 = repository.CreateAsnwerAsync(answer28, connection, transaction).Result;
                var answerId29 = repository.CreateAsnwerAsync(answer29, connection, transaction).Result;
                var answerId30 = repository.CreateAsnwerAsync(answer30, connection, transaction).Result;
                var answerId31 = repository.CreateAsnwerAsync(answer31, connection, transaction).Result;
                var answerId32 = repository.CreateAsnwerAsync(answer32, connection, transaction).Result;
                var answerId33 = repository.CreateAsnwerAsync(answer33, connection, transaction).Result;
                var answerId34 = repository.CreateAsnwerAsync(answer34, connection, transaction).Result;
                var answerId35 = repository.CreateAsnwerAsync(answer35, connection, transaction).Result;
                var answerId36 = repository.CreateAsnwerAsync(answer36, connection, transaction).Result;
                var answerId37 = repository.CreateAsnwerAsync(answer37, connection, transaction).Result;
                var answerId38 = repository.CreateAsnwerAsync(answer38, connection, transaction).Result;
                var answerId39 = repository.CreateAsnwerAsync(answer39, connection, transaction).Result;
                var answerId40 = repository.CreateAsnwerAsync(answer40, connection, transaction).Result;
                var answerId41 = repository.CreateAsnwerAsync(answer41, connection, transaction).Result;
                var answerId42 = repository.CreateAsnwerAsync(answer42, connection, transaction).Result;
                var answerId43 = repository.CreateAsnwerAsync(answer43, connection, transaction).Result;
                var answerId44 = repository.CreateAsnwerAsync(answer44, connection, transaction).Result;
                var answerId45 = repository.CreateAsnwerAsync(answer45, connection, transaction).Result;
                var answerId46 = repository.CreateAsnwerAsync(answer46, connection, transaction).Result;
                var answerId47 = repository.CreateAsnwerAsync(answer47, connection, transaction).Result;
                var answerId48 = repository.CreateAsnwerAsync(answer48, connection, transaction).Result;
                var answerId49 = repository.CreateAsnwerAsync(answer49, connection, transaction).Result;
                var answerId50 = repository.CreateAsnwerAsync(answer50, connection, transaction).Result;
                var answerId51 = repository.CreateAsnwerAsync(answer51, connection, transaction).Result;
                var answerId52 = repository.CreateAsnwerAsync(answer52, connection, transaction).Result;
                var answerId53 = repository.CreateAsnwerAsync(answer53, connection, transaction).Result;
                var answerId54 = repository.CreateAsnwerAsync(answer54, connection, transaction).Result;
                var answerId55 = repository.CreateAsnwerAsync(answer55, connection, transaction).Result;
                var answerId56 = repository.CreateAsnwerAsync(answer56, connection, transaction).Result;
                var answerId57 = repository.CreateAsnwerAsync(answer57, connection, transaction).Result;
                var answerId58 = repository.CreateAsnwerAsync(answer58, connection, transaction).Result;
                var answerId59 = repository.CreateAsnwerAsync(answer59, connection, transaction).Result;
                var answerId60 = repository.CreateAsnwerAsync(answer60, connection, transaction).Result;

                transaction.Commit();
            }
        }
    }

    #region Helper

    private static User ConstructUserEntity(string email, string password)
    {
        return new User()
        {
            Email = email,
            Password = CryptoHelper.EncryptPassword(password)
        };
    }

    private static Entities.Quiz ConstructQuizEntity(Guid userId, string title, bool isPublished)
    {
        return new Entities.Quiz()
        {
            UserId = userId,
            Title = title,
            IsPublished = isPublished
        };
    }

    private static Question ConstructQuestionEntity(Guid quizId, string text, QuestionType type)
    {
        return new Question()
        {
            QuizId = quizId,
            Text = text,
            Type = type
        };
    }

    private static Answer ConstructAnswerEntity(Guid questionId, string text, bool isCorrectAnswer)
    {
        return new Answer()
        {
            QuestionId = questionId,
            Text = text,
            IsCorrectAnswer = isCorrectAnswer
        };
    }

    private static Solution ConstructSolutionEntity(Guid userId, Guid quizId, Guid questionId, Guid answerId, double point)
    {
        return new Solution()
        {
            UserId = userId,
            QuizId = quizId,
            QuestionId = questionId,
            AnswerId = answerId,
            Point = point
        };
    }

    #endregion
}
