using Application.Validators.Common;
using Domain.DTOs.Major;
using FluentValidation;

namespace Application.Validators.Major;

public class CreateMajorRequestValidator : BaseValidator<CreateMajorRequest>
{
    public CreateMajorRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The name must not be empty");
    }
}