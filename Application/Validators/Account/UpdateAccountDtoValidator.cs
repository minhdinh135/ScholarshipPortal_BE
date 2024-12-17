using Application.Validators.Common;
using Domain.DTOs.Account;
using FluentValidation;

namespace Application.Validators.Account;

public class UpdateAccountDtoValidator : BaseValidator<UpdateAccountDto>
{
    public UpdateAccountDtoValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username must not be empty");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number must not empty");
    }
}