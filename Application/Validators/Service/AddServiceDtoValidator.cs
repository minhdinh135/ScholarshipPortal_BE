using Application.Validators.Common;
using Domain.DTOs.Service;
using FluentValidation;

namespace Application.Validators.Service;

public class AddServiceDtoValidator : BaseValidator<AddServiceDto>
{
    public AddServiceDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name must not be empty");

        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("Description length cannot exceed 200 characters");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0");

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Type must not be empty");
    }
}