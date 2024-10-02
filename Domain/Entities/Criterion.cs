namespace Domain.Entities;

public class Criterion : BaseEntity
{
    public string? Title { get; set; }
    
    public string? Description { get; set; }
    
    public int? ScholarshipProgramId { get; set; }
    
    public ScholarshipProgram? ScholarshipProgram { get; set; }
}