using Application.Validators.Common;
using Domain.DTOs.Feedback;
using FluentValidation;

namespace Application.Validators.Feedback;

public class UpdateFeedbackDtoValidator : BaseValidator<UpdateFeedbackDto>
{
    public UpdateFeedbackDtoValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content must not be empty");

        RuleFor(x => x.Rating)
            .GreaterThanOrEqualTo(0).WithMessage("Rating must be greater than or equal to 0");

        RuleFor(x => x.ApplicantId)
            .GreaterThan(0).WithMessage("ApplicantId must be greater than 0");

        RuleFor(x => x.ServiceId)
            .GreaterThan(0).WithMessage("ServiceId must be greater than 0");
    }
}