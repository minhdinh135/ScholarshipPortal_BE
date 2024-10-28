namespace Domain.DTOs.Provider;

public class UpdateProviderDetailsDto
{
    public string? OrganizationName { get; set; }

    public string? ContactPersonName { get; set; }

    public int? ProviderId { get; set; }

    public List<ProviderDocumentDto> ProviderDocuments { get; set; }
}