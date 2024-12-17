using Application.Validators.Common;
using Domain.DTOs.Application;
using FluentValidation;

namespace Application.Validators.Application;

public class UpdateApplicationDocumentDtoValidator : BaseValidator<UpdateApplicationDocumentDto>
{
    public UpdateApplicationDocumentDtoValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Type must not be empty");

        RuleFor(x => x.FileUrl)
            .NotEmpty().WithMessage("File URL must not be empty");

        RuleFor(x => x.ApplicationId)
            .GreaterThan(0).WithMessage("ApplicationId must be greater than 0");
    }
}