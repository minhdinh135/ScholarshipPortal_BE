using Application.Validators.Common;
using Domain.DTOs.Application;
using FluentValidation;

namespace Application.Validators.Application;

public class UpdateApplicationDtoValidator : BaseValidator<UpdateApplicationDto>
{
    public UpdateApplicationDtoValidator()
    {
        RuleFor(x => x.ApplicantId)
            .GreaterThan(0).WithMessage("ApplicantId must be greater than 0");

        RuleFor(x => x.ScholarshipProgramId)
            .GreaterThan(0).WithMessage("ScholarshipProgramId must be greater than 0");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status must not be empty");
    }
}