namespace Domain.Entities;

public class RequestDetail : BaseEntity
{
    public DateTime? ExpectedCompletionTime { get; set; }
    
    public string? ApplicationNotes { get; set; }
    
    public string? ScholarshipType { get; set; }
    
    public string? ApplicationFileUrl { get; set; }
    
    public int? RequestId { get; set; }
    
    public Request? Request { get; set; }
    
    public int? ServiceId { get; set; }
    
    public Service? Service { get; set; }
}