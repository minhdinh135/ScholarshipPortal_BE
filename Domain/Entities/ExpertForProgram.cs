namespace Domain.Entities;

public class ExpertForProgram 
{
    public int ScholarshipProgramId { get; set; }
    
    public ScholarshipProgram ScholarshipProgram { get; set; }
    
    public int ExpertId { get; set; }
    
    public Account Expert { get; set; }
}