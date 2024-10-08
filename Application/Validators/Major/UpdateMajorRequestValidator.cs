using System.Data;
using Application.Validators.Common;
using Domain.DTOs.Major;
using FluentValidation;

namespace Application.Validators.Major;

public class UpdateMajorRequestValidator : BaseValidator<UpdateMajorRequest>
{
    public UpdateMajorRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The name must not be empty");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("The description must not be empty");
    }
}