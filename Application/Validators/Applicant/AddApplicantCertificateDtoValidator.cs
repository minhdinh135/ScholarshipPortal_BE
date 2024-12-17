using Application.Validators.Common;
using Domain.DTOs.Applicant;
using FluentValidation;

namespace Application.Validators.Applicant;

public class AddApplicantCertificateDtoValidator : BaseValidator<AddApplicantCertificateDto>
{
    public AddApplicantCertificateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name must not be empty");

        RuleFor(x => x.ApplicantProfileId)
            .NotEmpty().WithMessage("ApplicantProfileId must not be empty");
    }
}