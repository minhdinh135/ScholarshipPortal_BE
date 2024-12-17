using Application.Validators.Common;
using Domain.DTOs.Subscription;
using FluentValidation;

namespace Application.Validators.Subscription;

public class UpdateSubscriptionDtoValidator : BaseValidator<UpdateSubscriptionDto>
{
    public UpdateSubscriptionDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name must not be empty");

        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("Description length cannot exceed 200 characters");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0");

        RuleFor(x => x.ValidMonths)
            .GreaterThan(0).WithMessage("ValidMonths must be greater than 0");

        RuleFor(x => x.NumberOfServices)
            .GreaterThan(0).WithMessage("NumberOfServices must be greater than 0");
    }
}