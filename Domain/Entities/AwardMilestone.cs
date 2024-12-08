using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class AwardMilestone : BaseEntity
{
    public DateTime FromDate { get; set; }
    
    public DateTime ToDate { get; set; }
    
    public decimal Amount { get; set; }
    
    [MaxLength(200)]
    public string? Note { get; set; }
    
    public int ScholarshipProgramId { get; set; }
    
    public ScholarshipProgram ScholarshipProgram { get; set; }
    
    public ICollection<AwardMilestoneDocument> AwardMilestoneDocuments { get; set; }
}