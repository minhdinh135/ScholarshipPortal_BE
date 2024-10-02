namespace Domain.Entities;

public class Feedback : BaseEntity
{
    public string? Content { get; set; }
    
    public double? Rating { get; set; } 
    
    public DateTime? FeedbackDate { get; set; }
    
    public int? FunderId { get; set; }
    
    public Account? Funder { get; set; }
    
    public int? ProviderId { get; set; }
    
    public Account? Provider { get; set; }
}