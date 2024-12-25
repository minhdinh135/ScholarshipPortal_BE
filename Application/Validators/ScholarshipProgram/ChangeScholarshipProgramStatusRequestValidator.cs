using Application.Helper;
using Application.Validators.Common;
using Domain.Constants;
using Domain.DTOs.ScholarshipProgram;
using FluentValidation;

namespace Application.Validators.ScholarshipProgram;

public class ChangeScholarshipProgramStatusRequestValidator : BaseValidator<ChangeScholarshipProgramStatusRequest>
{
    public ChangeScholarshipProgramStatusRequestValidator()
    {
        var validStatuses = EnumHelper.ConvertEnumToList<ScholarshipProgramStatusEnum>();
        
        RuleFor(x => x.Status)
            .Must(status => validStatuses.Contains(status))
            .WithMessage("Status only has following values: " + String.Join(", ", validStatuses));
    }
}