using Application.Validators.Common;
using Domain.DTOs.Application;
using FluentValidation;

namespace Application.Validators.Application;

public class AddApplicationDtoValidator : BaseValidator<AddApplicationDto>
{
    public AddApplicationDtoValidator()
    {
        RuleFor(x => x.Documents)
            .NotNull().WithMessage("Documents must not be null");
        
        RuleForEach(x => x.Documents)
            .NotNull().WithMessage("Documents cannot contain a null document")
            .SetValidator(new AddApplicationDocumentDtoValidator());
    }
}