using FluentValidation;
using Quiz.Engine.ViewModels.Requests;

namespace Quiz.Engine.Validators;

public class QuestionDetailRequestModelValidator : AbstractValidator<QuestionDetailRequestModel>
{
    public QuestionDetailRequestModelValidator()
    {
        RuleFor(x => x.Question)
            .NotNull()
            .MaximumLength(1000);
        
        RuleFor(x => x.Answers)
            .NotNull()
            .Must(x => x.Count() >= 1 && x.Count() <= 5)
            .WithMessage("Every question can have 1-5 possible answers");
    }
}
