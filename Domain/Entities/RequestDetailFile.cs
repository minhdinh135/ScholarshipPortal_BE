namespace Domain.Entities;

public class RequestDetailFile : BaseEntity
{
    public string? FileUrl { get; set; }
    
    public int? RequestDetailId { get; set; }
    
    public RequestDetail? RequestDetail { get; set; }
}