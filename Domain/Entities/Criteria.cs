namespace Domain.Entities;

public class Criteria : BaseEntity
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public int? ScholarshipProgramId { get; set; }
    
    public ScholarshipProgram? ScholarshipProgram { get; set; }
}