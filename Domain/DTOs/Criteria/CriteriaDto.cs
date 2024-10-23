using Domain.DTOs.Common;

namespace Domain.DTOs.Criteria;

public class CriteriaDto : BaseDto
{
    public int? Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? ScholarshipProgramId { get; set; }
}