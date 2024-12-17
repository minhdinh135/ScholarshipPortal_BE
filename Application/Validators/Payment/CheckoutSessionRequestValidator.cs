using Application.Validators.Common;
using Domain.DTOs.Payment;
using FluentValidation;

namespace Application.Validators.Payment;

public class CheckoutSessionRequestValidator : BaseValidator<CheckoutSessionRequest>
{
    public CheckoutSessionRequestValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0");

        RuleFor(x => x.ReceiverId)
            .GreaterThan(0).WithMessage("ReceiverId must be greater than 0");
    }
}