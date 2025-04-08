namespace Domain.DTOs.Funder;

public class AddFunderDetailsDto
{
    public string? OrganizationName { get; set; }

    public string? ContactPersonName { get; set; }

    public List<FunderDocumentDto> FunderDocuments { get; set; }
}