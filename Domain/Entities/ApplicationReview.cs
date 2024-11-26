namespace Domain.Entities;

public class ApplicationReview : BaseEntity
{
    public string? Description { get; set; }
    
    public int? Score { get; set; }
    
    public string? Comment { get; set; }
    
    public DateTime? ReviewDate { get; set; }
    
    public string? Status { get; set; }
    
    public int? ExpertId { get; set; }
    
    public Account? Expert { get; set; }
    
    public int? ApplicationId { get; set; }
    
    public Application? Application { get; set; }
}