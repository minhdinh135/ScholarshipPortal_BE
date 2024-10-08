using Application.Validators.Common;
using Domain.DTOs.Category;
using FluentValidation;

namespace Application.Validators.Category;

public class CreateCategoryRequestValidator : BaseValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The name must not be empty");
    }
}