namespace Domain.Entities;

public class ScholarshipProgramMajor
{
    public int? ScholarshipProgramId { get; set; }
    
    public ScholarshipProgram? ScholarshipProgram { get; set; }
    
    public int? MajorId { get; set; }
    
    public Major? Major { get; set; }
}