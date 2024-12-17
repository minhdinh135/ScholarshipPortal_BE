using Application.Validators.Common;
using Domain.DTOs.Account;
using FluentValidation;

namespace Application.Validators.Account;

public class UpdateWalletBankInformationDtoValidator : BaseValidator<UpdateWalletBankInformationDto>
{
    public UpdateWalletBankInformationDtoValidator()
    {
        RuleFor(x => x.BankAccountName)
            .NotEmpty().WithMessage("Bank account name must not be empty");

        RuleFor(x => x.BankAccountNumber)
            .NotEmpty().WithMessage("Bank account number must not be empty");
    }
}