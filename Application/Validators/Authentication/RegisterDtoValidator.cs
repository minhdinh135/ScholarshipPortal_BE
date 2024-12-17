using Application.Validators.Common;
using Domain.DTOs.Authentication;
using FluentValidation;

namespace Application.Validators.Authentication;

public class RegisterDtoValidator : BaseValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email must be in valid email format");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number must not be empty");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password must not be empty");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status must not be empty");

        RuleFor(x => x.RoleId)
            .GreaterThan(0).WithMessage("RoleId must be greater than 0");
    }
}