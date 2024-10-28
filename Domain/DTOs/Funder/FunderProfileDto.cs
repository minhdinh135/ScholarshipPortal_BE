using Domain.Entities;

namespace Domain.DTOs.Funder;

public class FunderProfileDto
{
    public int? Id { get; set; }
    
    public string? OrganizationName { get; set; }

    public string? ContactPersonName { get; set; }

    public int? FunderId { get; set; }
    
    public List<FunderDocumentDto> FunderDocuments { get; set; }
}