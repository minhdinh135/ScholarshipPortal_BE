using Application.Validators.Common;
using Domain.DTOs.Applicant;
using FluentValidation;

namespace Application.Validators.Applicant;

public class AddApplicantProfileDtoValidator : BaseValidator<AddApplicantProfileDto>
{
    public AddApplicantProfileDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName must not be empty");
    }
}