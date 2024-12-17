using Application.Validators.Common;
using Domain.DTOs.Request;
using FluentValidation;

namespace Application.Validators.Request;

public class ProviderUpdateRequestDtoValidator : BaseValidator<ProviderUpdateRequestDto>
{
    public ProviderUpdateRequestDtoValidator()
    {
        RuleForEach(x => x.ServiceResultDetails)
            .NotEmpty().WithMessage("Every ServiceResultDetail must not be empty");
    }
}