using Application.Validators.Common;
using Domain.DTOs.Criteria;
using FluentValidation;

namespace Application.Validators.Criteria;

public class UpdateCriteriaRequestValidator : BaseValidator<UpdateCriteriaRequest>
{
    public UpdateCriteriaRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("The title must not be empty");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("The description must not be empty");

        RuleFor(x => x.ScholarshipProgramId)
            .NotEmpty().WithMessage("The scholarship program id is required");
    }
}