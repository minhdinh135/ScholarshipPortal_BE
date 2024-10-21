namespace Domain.Entities;

public class FunderDocument : BaseEntity
{
    public string? Name { get; set; }
    
    public string? Type { get; set; }
    
    public string? FileUrl { get; set; }
    
    public int? FunderProfileId { get; set; }
    
    public FunderProfile? FunderProfile { get; set; }
}