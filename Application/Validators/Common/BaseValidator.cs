using FluentValidation;
using FluentValidation.Results;

namespace Application.Validators.Common;

public class BaseValidator<T> : AbstractValidator<T> where T : class
{
    protected override bool PreValidate(ValidationContext<T> context, ValidationResult result)
    {
        if (context.InstanceToValidate == null)
        {
            result.Errors.Add(new ValidationFailure("", "Please ensure a model was supplied."));
            return false;
        }

        return true;
    }
}