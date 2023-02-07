using FluentValidation;
using Quiz.Engine.ViewModels.Requests;

namespace Quiz.Engine.Validators;

public class QuizDetailRequestModelValidator : AbstractValidator<QuizDetailRequestModel>
{
    public QuizDetailRequestModelValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(1000);
        
        RuleFor(x => x.Questions)
            .NotNull()
            .Must(x => x.Count() >= 1 && x.Count() <= 10)
            .WithMessage("A quiz can have 1-10 questions");
    }
}
