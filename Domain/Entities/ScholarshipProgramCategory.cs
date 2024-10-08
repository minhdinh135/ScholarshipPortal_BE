namespace Domain.Entities;

public class ScholarshipProgramCategory
{
    public int? ScholarshipProgramId { get; set; }
    
    public ScholarshipProgram? ScholarshipProgram { get; set; }
    
    public int? CategoryId { get; set; }
    
    public Category? Category { get; set; }
}