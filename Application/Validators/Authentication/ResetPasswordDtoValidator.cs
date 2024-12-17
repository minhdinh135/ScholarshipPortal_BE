using Application.Validators.Common;
using Domain.DTOs.Authentication;
using FluentValidation;

namespace Application.Validators.Authentication;

public class ResetPasswordDtoValidator : BaseValidator<ResetPasswordDto>
{
    public ResetPasswordDtoValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email must be in valid email format");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("NewPassword must not be empty");
    }
}