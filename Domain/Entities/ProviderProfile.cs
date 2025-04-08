using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class ProviderProfile : BaseEntity
{
    [MaxLength(100)]
    public string OrganizationName { get; set; }
    
    [MaxLength(100)]
    public string? ContactPersonName { get; set; }
    
    public int ProviderId { get; set; }
    
    public Account Provider { get; set; }
    
    public ICollection<ProviderDocument>? ProviderDocuments { get; set; }
}