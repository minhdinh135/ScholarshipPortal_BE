using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Service : BaseEntity
{
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    [MaxLength(100)]
    public string Type { get; set; }
    
    public decimal? Price { get; set; }
    
    [MaxLength(100)]
    public string Status { get; set; }
    
    public int ProviderId { get; set; }
    
    public Account Provider { get; set; }
    
    public ICollection<Feedback>? Feedbacks { get; set; }
    
    public ICollection<RequestDetail>? RequestDetails { get; set; }
}