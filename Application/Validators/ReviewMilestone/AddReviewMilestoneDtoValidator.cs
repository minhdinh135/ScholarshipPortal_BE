using Application.Validators.Common;
using Domain.DTOs.ReviewMilestone;
using FluentValidation;

namespace Application.Validators.ReviewMilestone;

public class AddReviewMilestoneDtoValidator : BaseValidator<AddReviewMilestoneDto>
{
    public AddReviewMilestoneDtoValidator()
    {
        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("Description length cannot exceed 200 characters");

        RuleFor(x => x.ScholarshipProgramId)
            .GreaterThan(0).WithMessage("ScholarshipProgramId must be greater than 0");
    }
}