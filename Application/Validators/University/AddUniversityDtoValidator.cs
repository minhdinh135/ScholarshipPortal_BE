using Application.Validators.Common;
using Domain.DTOs.University;
using FluentValidation;

namespace Application.Validators.University;

public class AddUniversityDtoValidator : BaseValidator<AddUniversityDto>
{
    public AddUniversityDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name must not be empty");

        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("Description length cannot exceed 200 characters");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City must not be empty");

        RuleFor(x => x.CountryId)
            .GreaterThan(0).WithMessage("CountryId must be greater than 0");
    }
}