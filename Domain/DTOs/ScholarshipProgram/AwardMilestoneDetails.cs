namespace Domain.DTOs.ScholarshipProgram;

public class AwardMilestoneDetails
{
    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public decimal? Amount { get; set; }
    
    public List<string> DocumentTypes { get; set; }
}