using Application.Validators.Common;
using Domain.DTOs.Authentication;
using FluentValidation;

namespace Application.Validators.Authentication;

public class LoginDtoValidator : BaseValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email must be in valid email format");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password must not be empty");
    }
}