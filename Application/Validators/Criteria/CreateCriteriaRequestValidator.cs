using Application.Validators.Common;
using Domain.DTOs.Criteria;
using FluentValidation;

namespace Application.Validators.Criteria;

public class CreateCriteriaRequestValidator : BaseValidator<CreateCriteriaRequest>
{
    public CreateCriteriaRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("The title must not be empty");
    }
}