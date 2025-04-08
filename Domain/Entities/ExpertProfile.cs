using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class ExpertProfile : BaseEntity
{
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    [MaxLength(100)]
    public string Major { get; set; }
    
    public int ExpertId { get; set; }
    
    public Account Expert { get; set; }
}