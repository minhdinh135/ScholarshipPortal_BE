namespace Domain.Entities;

public class Review : BaseEntity
{
    public string? Comment { get; set; }
    
    public double? Score { get; set; }
    
    public DateTime? ReviewedDate { get; set; }
    
    public int? ProviderId { get; set; }
    
    public Account? Provider { get; set; }
    
    public int? ApplicationId { get; set; }
    
    public Application? Application { get; set; }
}