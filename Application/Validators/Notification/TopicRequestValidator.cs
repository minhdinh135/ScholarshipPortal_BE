using Application.Validators.Common;
using Domain.DTOs.Notification;
using FluentValidation;

namespace Application.Validators.Notification;

public class TopicRequestValidator : BaseValidator<TopicRequest>
{
    public TopicRequestValidator()
    {
        RuleFor(x => x.Topic)
            .NotEmpty().WithMessage("Topic must not be empty");

        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token must not be empty");
    }
}