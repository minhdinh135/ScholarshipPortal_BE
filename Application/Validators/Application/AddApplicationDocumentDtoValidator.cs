using Application.Validators.Common;
using Domain.DTOs.Application;
using FluentValidation;

namespace Application.Validators.Application;

public class AddApplicationDocumentDtoValidator : BaseValidator<AddApplicationDocumentDto>
{
    public AddApplicationDocumentDtoValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Type must not be empty");

        RuleFor(x => x.FileUrl)
            .NotEmpty().WithMessage("File URL must not be empty");
    }
}