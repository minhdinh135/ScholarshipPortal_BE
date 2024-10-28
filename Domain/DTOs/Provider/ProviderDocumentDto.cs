namespace Domain.DTOs.Provider;

public class ProviderDocumentDto
{
    public int? Id { get; set; }
    
    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? FileUrl { get; set; }

    public int? ProviderProfileId { get; set; }
}