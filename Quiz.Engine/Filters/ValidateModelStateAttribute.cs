using Microsoft.AspNetCore.Mvc.Filters;
using Quiz.Engine.Service;

namespace Quiz.Engine.Filters;

public class ValidateModelStateAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage)
                    .ToList();

            throw new ModelValidationFailedException(string.Join(Environment.NewLine, errors));
        }
    }
}