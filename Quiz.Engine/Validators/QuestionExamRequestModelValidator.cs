using FluentValidation;
using Quiz.Engine.Data.Enums;
using Quiz.Engine.Data.Repositories.Contracts;
using Quiz.Engine.Service;
using Quiz.Engine.ViewModels.Requests;

namespace Quiz.Engine.Validators;

public class QuestionExamRequestModelValidator : AbstractValidator<QuestionExamRequestModel>
{
    private readonly IQuizEngineRepository _quizEngineRepository;

    public QuestionExamRequestModelValidator(IQuizEngineRepository quizEngineRepository)
    {
        _quizEngineRepository = quizEngineRepository;

        RuleFor(x => x.Id)
            .Must(x => Guid.TryParse(x, out _))
            .WithMessage("Id is not a valid quid");

        When(x => x.Answers is not null, () =>
            When(x => IsSingleQuestion(x.Id), () =>
                RuleFor(x => x.Answers)
                .Must(x => x.Count() == 1 || x.Count() == 0)
                .WithMessage("Single type question can 1 answer maximum")
            ).Otherwise(() =>
                RuleFor(x => x.Answers)
                .Must(x => x.Count() <= 5)
                .WithMessage("Multiple type question can 5 answers maximum")
            )
        );

        RuleForEach(x => x.Answers)
            .Must(x => Guid.TryParse(x, out _))
            .WithMessage("Answer is not a valid quid");

        RuleFor(x => x.Answers)
            .Must(x => x.Count() == x.Distinct().Count())
            .WithMessage("Single question cannot contain same answers");
    }

    #region Helper

    private bool IsSingleQuestion(string questionId)
    {
        var questionType = _quizEngineRepository.GetQuestionTypeAsync(Guid.Parse(questionId)).Result;

        if (questionType is null)
        {
            throw new QuestionRecordNotFoundException();
        }

        return questionType == QuestionType.Single;
    }

    #endregion
}
