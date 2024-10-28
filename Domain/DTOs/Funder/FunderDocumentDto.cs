namespace Domain.DTOs.Funder;

public class FunderDocumentDto
{
    public int? Id { get; set; }
    
    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? FileUrl { get; set; }

    public int? FunderProfileId { get; set; }
}