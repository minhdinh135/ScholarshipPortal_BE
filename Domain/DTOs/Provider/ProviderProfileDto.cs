namespace Domain.DTOs.Provider;

public class ProviderProfileDto
{
    public int? Id { get; set; }
    
    public string? OrganizationName { get; set; }

    public string? ContactPersonName { get; set; }

    public int? ProviderId { get; set; }
    
    public List<ProviderDocumentDto> ProviderDocuments { get; set; }
}