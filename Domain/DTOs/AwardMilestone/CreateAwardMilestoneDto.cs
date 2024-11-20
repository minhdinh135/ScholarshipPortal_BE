namespace Domain.DTOs.AwardMilestone;

public class CreateAwardMilestoneDto
{
    public DateTime? FromDate { get; set; }
    
    public DateTime? ToDate { get; set; }
    
    public decimal? Amount { get; set; }
    
    public int? ScholarshipProgramId { get; set; }
}
