using Application.Validators.Common;
using Domain.DTOs.Account;
using FluentValidation;

namespace Application.Validators.Account;

public class UpdateWalletBalanceDtoValidator : BaseValidator<UpdateWalletBalanceDto>
{
    public UpdateWalletBalanceDtoValidator()
    {
        RuleFor(x => x.Balance)
            .GreaterThanOrEqualTo(0).WithMessage("Balance must be greater than or equal to 0");
    }
}