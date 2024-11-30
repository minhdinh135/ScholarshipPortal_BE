namespace Domain.DTOs.Provider;

public class AddProviderDetailsDto
{
    public string? OrganizationName { get; set; }

    public string? ContactPersonName { get; set; }

    public List<ProviderDocumentDto> ProviderDocuments { get; set; }
}