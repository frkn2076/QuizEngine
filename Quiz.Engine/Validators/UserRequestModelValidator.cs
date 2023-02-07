using FluentValidation;
using Quiz.Engine.ViewModels.Requests;

namespace Quiz.Engine.Validators;

public class UserRequestModelValidator : AbstractValidator<UserRequestModel>
{
    public UserRequestModelValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .MaximumLength(100);
        
        RuleFor(x => x.Password)
            .Length(4, 100);
    }
}
