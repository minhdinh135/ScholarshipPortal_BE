namespace Domain.DTOs.Criteria;

public class CreateCriteriaRequest 
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? ScholarshipProgramId { get; set; }
}