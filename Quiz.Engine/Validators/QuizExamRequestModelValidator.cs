using FluentValidation;
using Quiz.Engine.Data.Repositories.Contracts;
using Quiz.Engine.ViewModels.Requests;

namespace Quiz.Engine.Validators;

public class QuizExamRequestModelValidator : AbstractValidator<QuizExamRequestModel>
{
    private readonly IQuizEngineRepository _quizEngineRepository;

    public QuizExamRequestModelValidator(IQuizEngineRepository quizEngineRepository)
    {
        _quizEngineRepository = quizEngineRepository;

        RuleFor(x => x.Id)
            .Must(x => Guid.TryParse(x, out _))
            .WithMessage("Id is not a valid quid");

        When(x => x.Questions is not null, () =>
            RuleFor(x => x.Questions)
            .Must(x => x.Count() <= 10)
            .WithMessage("A quiz can have 10 questions maximum")
        );

        RuleFor(x => x.Questions)
            .Must(x => x.Select(y => y.Id).Count() == x.Select(y => y.Id).Distinct().Count())
            .WithMessage("Single quiz cannot contain same questions");

        RuleForEach(x => x.Questions).SetValidator(new QuestionExamRequestModelValidator(_quizEngineRepository));
    }
}
