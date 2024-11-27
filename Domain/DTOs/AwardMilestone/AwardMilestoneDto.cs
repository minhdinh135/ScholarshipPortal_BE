namespace Domain.DTOs.AwardMilestone;

public class AwardMilestoneDto 
{
    public int Id { get; set; }

    public DateTime? FromDate { get; set; }
    
    public DateTime? ToDate { get; set; }
    
    public decimal? Amount { get; set; }
    
    public string? Note { get; set; }
    
    public int? ScholarshipProgramId { get; set; }
}
