using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Assignment : BaseEntity
{
    public int ScholarshipProgramId { get; set; }
    
    public ScholarshipProgram ScholarshipProgram { get; set; }
    
    public int ExpertId { get; set; }
    
    public Account Expert { get; set; }
    
    [MaxLength(100)]
    public string Status { get; set; }
}