using Application.Validators.Common;
using Domain.DTOs.Request;
using FluentValidation;

namespace Application.Validators.Request;

public class ApplicantCreateRequestDtoValidator : BaseValidator<ApplicantCreateRequestDto>
{
    public ApplicantCreateRequestDtoValidator()
    {
        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("Description length cannot exceed 200 characters");

        RuleFor(x => x.ApplicantId)
            .GreaterThan(0).WithMessage("ApplicantId must be greater than 0");

        RuleForEach(x => x.ServiceIds)
            .GreaterThan(0).WithMessage("Every service must be greater than 0");
    }
}