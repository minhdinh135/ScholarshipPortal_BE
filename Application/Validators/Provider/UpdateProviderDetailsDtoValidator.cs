using Application.Validators.Common;
using Domain.DTOs.Provider;
using FluentValidation;

namespace Application.Validators.Provider;

public class UpdateProviderDetailsDtoValidator : BaseValidator<UpdateProviderDetailsDto>
{
    public UpdateProviderDetailsDtoValidator()
    {
        RuleFor(x => x.OrganizationName)
            .NotEmpty().WithMessage("OrganizationName must not be empty");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone must not be empty");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status must not be empty");

        RuleFor(x => x.SubscriptionName)
            .NotEmpty().WithMessage("SubscriptionName must not be empty");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username must not be empty");
    }
}