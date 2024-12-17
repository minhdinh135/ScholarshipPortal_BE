using Application.Validators.Common;
using Domain.DTOs.Account;
using FluentValidation;

namespace Application.Validators.Account;

public class CreateWalletDtoValidator : BaseValidator<CreateWalletDto>
{
    public CreateWalletDtoValidator()
    {
        RuleFor(x => x.BankAccountName)
            .NotEmpty().WithMessage("Bank account name must not be empty");

        RuleFor(x => x.BankAccountNumber)
            .NotEmpty().WithMessage("Bank account number must not be empty");

        RuleFor(x => x.Balance)
            .GreaterThanOrEqualTo(0).WithMessage("Balance must be greater than or equal to 0");
    }
}