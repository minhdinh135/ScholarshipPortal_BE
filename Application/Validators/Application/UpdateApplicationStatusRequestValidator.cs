using Application.Validators.Common;
using Domain.DTOs.Application;
using FluentValidation;

namespace Application.Validators.Application;

public class UpdateApplicationStatusRequestValidator : BaseValidator<UpdateApplicationStatusRequest>
{
    public UpdateApplicationStatusRequestValidator()
    {
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status must not be empty");
    }
}