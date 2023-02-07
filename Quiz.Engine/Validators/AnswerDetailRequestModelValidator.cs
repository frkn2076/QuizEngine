using FluentValidation;
using Quiz.Engine.ViewModels.Requests;

namespace Quiz.Engine.Validators;

public class AnswerDetailRequestModelValidator : AbstractValidator<AnswerDetailRequestModel>
{
    public AnswerDetailRequestModelValidator()
    {
        RuleFor(x => x.Answer)
            .NotNull()
            .MaximumLength(1000);
    }
}
