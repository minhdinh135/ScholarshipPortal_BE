using Application.Validators.Common;
using Domain.DTOs.Notification;
using FluentValidation;

namespace Application.Validators.Notification;

public class NotificationUpdateDTOValidator : BaseValidator<NotificationUpdateDTO>
{
    public NotificationUpdateDTOValidator()
    {
        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Message must not be empty");

        RuleFor(x => x.ReceiverId)
            .GreaterThan(0).WithMessage("ReceiverId must be greater than 0");
    }
}