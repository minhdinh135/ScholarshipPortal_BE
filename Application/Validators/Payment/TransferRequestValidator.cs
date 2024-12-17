using Application.Validators.Common;
using Domain.DTOs.Payment;
using FluentValidation;

namespace Application.Validators.Payment;

public class TransferRequestValidator : BaseValidator<TransferRequest>
{
    public TransferRequestValidator()
    {
        RuleFor(x => x.SenderId)
            .GreaterThan(0).WithMessage("SenderId must be greater than 0");

        RuleFor(x => x.ReceiverId)
            .GreaterThan(0).WithMessage("ReceiverId must be greater than 0");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0");

        RuleFor(x => x.PaymentMethod)
            .NotEmpty().WithMessage("PaymentMethod must not be empty");

        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("Description length cannot exceed 200 characters");
    }
}