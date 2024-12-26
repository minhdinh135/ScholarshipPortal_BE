using Domain.DTOs.AwardMilestone;

namespace Domain.DTOs.ScholarshipProgram;

public class AwardMilestoneDetails
{
    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public decimal? Amount { get; set; }
    
    public string? Note { get; set; }
    
    public List<AwardMilestoneDocumentDto> AwardMilestoneDocuments { get; set; }
}