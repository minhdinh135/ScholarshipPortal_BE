using Application.Validators.Common;
using Domain.DTOs.Authentication;
using FluentValidation;

namespace Application.Validators.Authentication;

public class ChangePasswordDtoValidator : BaseValidator<ChangePasswordDto>
{
    public ChangePasswordDtoValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email must be in valid email format");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("NewPassword must not be empty");

        RuleFor(x => x.OldPassword)
            .NotEmpty().WithMessage("OldPassword must not be empty");
    }
}