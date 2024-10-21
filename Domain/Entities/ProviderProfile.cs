namespace Domain.Entities;

public class ProviderProfile : BaseEntity
{
    public string? OrganizationName { get; set; }
    
    public string? ContactPersonName { get; set; }
    
    public int? ProviderId { get; set; }
    
    public Account? Provider { get; set; }
    
    public ICollection<ProviderDocument>? ProviderDocuments { get; set; }
}