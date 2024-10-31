
namespace Domain.DTOs.ReviewMilestone;

public class AddReviewMilestoneDto 
{
    public string? Description { get; set; }
    
    public DateTime? FromDate { get; set; }
    
    public DateTime? ToDate { get; set; }
    
    public int? ScholarshipProgramId { get; set; }
}
