namespace Domain.DTOs.Funder;

public class UpdateFunderDetailsDto
{
    public string? OrganizationName { get; set; }

    public string? ContactPersonName { get; set; }

    public int? FunderId { get; set; }

    public List<FunderDocumentDto> FunderDocuments { get; set; }
}