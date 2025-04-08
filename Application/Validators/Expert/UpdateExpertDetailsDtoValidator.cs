using Application.Validators.Common;
using Domain.DTOs.Expert;
using FluentValidation;

namespace Application.Validators.Expert;

public class UpdateExpertDetailsDtoValidator : BaseValidator<UpdateExpertDetailsDto>
{
    public UpdateExpertDetailsDtoValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username must not be empty");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone must not be empty");

        RuleFor(x => x.Major)
            .NotEmpty().WithMessage("Major must not be empty");
    }
}