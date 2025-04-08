using Application.Validators.Common;
using Domain.DTOs.Expert;
using FluentValidation;

namespace Application.Validators.Expert;

public class CreateExpertDetailsDtoValidator : BaseValidator<CreateExpertDetailsDto>
{
    public CreateExpertDetailsDtoValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email must be in valid email format");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password must not be empty");

        RuleFor(x => x.Major)
            .NotEmpty().WithMessage("Major must not be empty");
    }
}