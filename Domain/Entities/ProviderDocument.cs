namespace Domain.Entities;

public class ProviderDocument : BaseEntity
{
    public string? Name { get; set; }
    
    public string? Type { get; set; }
    
    public string? FileUrl { get; set; }
    
    public int? ProviderProfileId { get; set; }
    
    public ProviderProfile? ProviderProfile { get; set; }
}