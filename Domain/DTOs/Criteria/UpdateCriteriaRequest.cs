using Domain.DTOs.Common;

namespace Domain.DTOs.Criteria;

public class UpdateCriteriaRequest : BaseUpdateRequest
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public int? ScholarshipProgramId { get; set; }
}