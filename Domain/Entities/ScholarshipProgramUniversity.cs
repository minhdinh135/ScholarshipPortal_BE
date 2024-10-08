namespace Domain.Entities;

public class ScholarshipProgramUniversity
{
    public int? ScholarshipProgramId { get; set; }
    
    public ScholarshipProgram? ScholarshipProgram { get; set; }
    
    public int? UniversityId { get; set; }
    
    public University? University { get; set; }
}