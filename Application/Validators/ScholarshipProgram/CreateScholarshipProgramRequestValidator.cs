using Application.Validators.Common;
using Domain.DTOs.ScholarshipProgram;
using FluentValidation;

namespace Application.Validators.ScholarshipProgram;

public class CreateScholarshipProgramRequestValidator : BaseValidator<CreateScholarshipProgramRequest>
{
    public CreateScholarshipProgramRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The name must not be empty");

        RuleFor(x => x.ScholarshipAmount)
            .NotEmpty().WithMessage("The scholarship amount is required");

        RuleFor(x => x.NumberOfScholarships)
            .NotEmpty().WithMessage("The number of scholarships is required");

        RuleFor(x => x.FunderId)
            .NotEmpty().WithMessage("The funder id is required");
    }
}