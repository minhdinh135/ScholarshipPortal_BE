using Application.Validators.Common;
using Domain.DTOs.Payment;
using FluentValidation;

namespace Application.Validators.Payment;

public class AddTransactionDtoValidator : BaseValidator<AddTransactionDto>
{
    public AddTransactionDtoValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0).WithMessage("Amount must be greater than or equal to 0");

        RuleFor(x => x.ReceiverId)
            .GreaterThan(0).WithMessage("ReceiverId must be greater than 0");

        RuleFor(x => x.PaymentMethod)
            .NotEmpty().WithMessage("PaymentMethod must not be empty");
    }
}