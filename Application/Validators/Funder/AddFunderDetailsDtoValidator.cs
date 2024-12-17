using Application.Validators.Common;
using Domain.DTOs.Funder;
using FluentValidation;

namespace Application.Validators.Funder;

public class AddFunderDetailsDtoValidator : BaseValidator<AddFunderDetailsDto>
{
    public AddFunderDetailsDtoValidator()
    {
        RuleFor(x => x.OrganizationName)
            .NotEmpty().WithMessage("OrganizationName must not be empty");

        RuleForEach(x => x.FunderDocuments)
            .NotEmpty().WithMessage("Every document cannot be empty");
    }
}