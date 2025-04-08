using Application.Validators.Common;
using Domain.DTOs.Provider;
using FluentValidation;

namespace Application.Validators.Provider;

public class AddProviderDetailsDtoValidator : BaseValidator<AddProviderDetailsDto>
{
    public AddProviderDetailsDtoValidator()
    {
        RuleFor(x => x.OrganizationName)
            .NotEmpty().WithMessage("OrganizationName must not be empty");

        RuleForEach(x => x.ProviderDocuments)
            .NotEmpty().WithMessage("Every document cannot be empty");
    }
}