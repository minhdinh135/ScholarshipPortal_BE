using Application.Validators.Common;
using Domain.DTOs.Application;
using FluentValidation;

namespace Application.Validators.Application;

public class UpdateReviewResultDtoValidator : BaseValidator<UpdateReviewResultDto>
{
    public UpdateReviewResultDtoValidator()
    {
        RuleFor(x => x.ApplicationReviewId)
            .NotEmpty().WithMessage("ApplicationReviewId must not be empty");

        RuleFor(x => x.Comment)
            .MaximumLength(200).WithMessage("Comment must not exceed 200 characters");

        RuleFor(x => x.Score)
            .GreaterThanOrEqualTo(0).WithMessage("Score must not be negative")
            .LessThanOrEqualTo(100).WithMessage("Score must be less than or equal to 100");
    }
}