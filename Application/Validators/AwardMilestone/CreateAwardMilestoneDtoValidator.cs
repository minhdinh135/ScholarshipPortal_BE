using Application.Validators.Common;
using Domain.DTOs.AwardMilestone;
using FluentValidation;

namespace Application.Validators.AwardMilestone;

public class CreateAwardMilestoneDtoValidator : BaseValidator<CreateAwardMilestoneDto>
{
    public CreateAwardMilestoneDtoValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0");

        RuleFor(x => x.Note)
            .MaximumLength(200).WithMessage("Note must not exceed 200 characters");

        RuleFor(x => x.ScholarshipProgramId)
            .GreaterThan(0).WithMessage("ScholarshipProgramId must be greater than 0");

        RuleForEach(x => x.AwardMilestoneDocuments).ChildRules(document =>
        {
            document.RuleFor(x => x.Type)
                .NotEmpty().WithMessage("AwardMilestoneDocument Type must not be empty");
        });
    }
}