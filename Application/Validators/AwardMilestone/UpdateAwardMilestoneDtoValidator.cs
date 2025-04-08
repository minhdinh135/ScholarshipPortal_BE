using Application.Validators.Common;
using Domain.DTOs.AwardMilestone;
using FluentValidation;

namespace Application.Validators.AwardMilestone;

public class UpdateAwardMilestoneDtoValidator : BaseValidator<UpdateAwardMilestoneDto>
{
    public UpdateAwardMilestoneDtoValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0");

        RuleFor(x => x.Note)
            .MaximumLength(200).WithMessage("Note must not exceed 200 characters");

        RuleFor(x => x.ScholarshipProgramId)
            .GreaterThan(0).WithMessage("ScholarshipProgramId must be greater than 0");
    }
}