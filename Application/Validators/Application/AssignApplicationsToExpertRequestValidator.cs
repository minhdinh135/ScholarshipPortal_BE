using Application.Validators.Common;
using Domain.DTOs.Application;
using FluentValidation;

namespace Application.Validators.Application;

public class AssignApplicationsToExpertRequestValidator : BaseValidator<AssignApplicationsToExpertRequest>
{
    public AssignApplicationsToExpertRequestValidator()
    {
        RuleFor(x => x.ExpertId)
            .GreaterThan(0).WithMessage("Expert Id must be greater than 0");

        RuleFor(x => x.ApplicationIds)
            .NotEmpty().WithMessage("ApplicationIds must not be empty");
    }
}