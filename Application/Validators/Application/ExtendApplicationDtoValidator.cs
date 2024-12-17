using Application.Validators.Common;
using Domain.DTOs.Application;
using FluentValidation;

namespace Application.Validators.Application;

public class ExtendApplicationDtoValidator : BaseValidator<ExtendApplicationDto>
{
    public ExtendApplicationDtoValidator()
    {
        RuleFor(x => x.ApplicationId)
            .GreaterThan(0).WithMessage("ApplicationId must be greater than 0");

        RuleForEach(x => x.Documents).SetValidator(new AddApplicationDocumentDtoValidator());
    }
}