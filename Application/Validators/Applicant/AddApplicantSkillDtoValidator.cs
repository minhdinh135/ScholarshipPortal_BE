using Application.Validators.Common;
using Domain.DTOs.Applicant;
using FluentValidation;

namespace Application.Validators.Applicant;

public class AddApplicantSkillDtoValidator : BaseValidator<AddApplicantSkillDto>
{
    public AddApplicantSkillDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name must not be empty");
    }
}