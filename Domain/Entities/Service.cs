namespace Domain.Entities;

public class Service : BaseEntity
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public string? Type { get; set; }
    
    public decimal? Price { get; set; }
    
    public string? Status { get; set; }
    
    public int? ProviderId { get; set; }
    
    public Account? Provider { get; set; }
    
    public ICollection<Feedback>? Feedbacks { get; set; }
    
    public ICollection<RequestDetail>? RequestDetails { get; set; }
}