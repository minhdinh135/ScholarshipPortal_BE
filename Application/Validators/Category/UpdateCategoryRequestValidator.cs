using Application.Validators.Common;
using Domain.DTOs.Category;
using FluentValidation;

namespace Application.Validators.Category;

public class UpdateCategoryRequestValidator : BaseValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The name must not be empty");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("The description must not be empty");
    }
}