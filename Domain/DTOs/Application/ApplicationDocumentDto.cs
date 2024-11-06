namespace Domain.DTOs.Application;

public class ApplicationDocumentDto
{
    public int? Id { get; set; }
    
    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? FileUrl { get; set; }

    public int? ApplicationId { get; set; }
}
