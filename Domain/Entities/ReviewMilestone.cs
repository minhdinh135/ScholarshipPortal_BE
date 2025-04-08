using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class ReviewMilestone : BaseEntity
{
    [MaxLength(200)]
    public string Description { get; set; }
    
    public DateTime FromDate { get; set; }
    
    public DateTime ToDate { get; set; }
    
    public int ScholarshipProgramId { get; set; }
    
    public ScholarshipProgram ScholarshipProgram { get; set; }
}