using Application.Validators.Common;
using Domain.DTOs.Authentication;
using FluentValidation;

namespace Application.Validators.Authentication;

public class SendOtpDtoValidator : BaseValidator<SendOtpDto>
{
    public SendOtpDtoValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email must be in valid email format");
    }
}