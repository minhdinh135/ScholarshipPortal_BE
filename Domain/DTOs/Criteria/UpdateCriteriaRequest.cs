namespace Domain.DTOs.Criteria;

public class UpdateCriteriaRequest 
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? ScholarshipProgramId { get; set; }
}