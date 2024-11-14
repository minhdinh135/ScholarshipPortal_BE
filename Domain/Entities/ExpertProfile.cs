namespace Domain.Entities;

public class ExpertProfile : BaseEntity
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public string? Major { get; set; }
    
    public int? ExpertId { get; set; }
    
    public Account? Expert { get; set; }
}