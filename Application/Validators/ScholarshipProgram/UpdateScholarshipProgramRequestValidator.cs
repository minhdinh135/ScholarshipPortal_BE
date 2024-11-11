using Application.Validators.Common;
using Domain.DTOs.ScholarshipProgram;
using FluentValidation;

namespace Application.Validators.ScholarshipProgram;

public class UpdateScholarshipProgramRequestValidator : BaseValidator<UpdateScholarshipProgramRequest>
{
    public UpdateScholarshipProgramRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The name must not be empty");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("The description must not be empty");

        RuleFor(x => x.ScholarshipAmount)
            .NotEmpty().WithMessage("The scholarship amount is required");

        RuleFor(x => x.NumberOfScholarships)
            .NotEmpty().WithMessage("The number of scholarships is required");

        RuleFor(x => x.Deadline)
            .NotEmpty().WithMessage("The deadline is required");

        RuleFor(x => x.FunderId)
            .NotEmpty().WithMessage("The funder id is required");

        RuleFor(x => x.UniversityIds)
            .NotEmpty().WithMessage("List of university ids is required");
    }
}