using Domain.DTOs.Common;

namespace Domain.DTOs.ReviewMilestone;

public class ReviewMilestoneDto : BaseDto
{
    public int Id { get; set; }

    public string? Description { get; set; }
    
    public DateTime? FromDate { get; set; }
    
    public DateTime? ToDate { get; set; }
    
    public int? ScholarshipProgramId { get; set; }
}
