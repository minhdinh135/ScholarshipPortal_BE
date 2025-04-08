using Application.Validators.Common;
using Domain.DTOs.Notification;
using FluentValidation;

namespace Application.Validators.Notification;

public class RejectNotificationRequestValidator : BaseValidator<RejectNotificationRequest>
{
    public RejectNotificationRequestValidator()
    {
        RuleFor(x => x.Topic)
            .NotEmpty().WithMessage("Topic must not be empty");

        RuleFor(x => x.Body)
            .NotEmpty().WithMessage("Body must not be empty");
    }
}