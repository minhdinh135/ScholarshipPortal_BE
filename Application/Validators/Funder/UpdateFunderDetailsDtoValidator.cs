using Application.Validators.Common;
using Domain.DTOs.Funder;
using FluentValidation;

namespace Application.Validators.Funder;

public class UpdateFunderDetailsDtoValidator : BaseValidator<UpdateFunderDetailsDto>
{
    public UpdateFunderDetailsDtoValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username must not be empty");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone must not be empty");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status must not be empty");

        RuleFor(x => x.OrganizationName)
            .NotEmpty().WithMessage("OrganizationName must not be empty");
    }
}