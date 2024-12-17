using Application.Validators.Common;
using Domain.DTOs.Authentication;
using FluentValidation;

namespace Application.Validators.Authentication;

public class VerifyOtpDtoValidator : BaseValidator<VerifyOtpDto>
{
    public VerifyOtpDtoValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email must be in valid email format");

        RuleFor(x => x.Otp)
            .NotEmpty().WithMessage("Otp must not be empty");
    }
}