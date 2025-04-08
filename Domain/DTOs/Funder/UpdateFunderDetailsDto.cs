namespace Domain.DTOs.Funder;

public class UpdateFunderDetailsDto
{
    public string Avatar { get; set; }

    public string Username { get; set; }

    public string Phone { get; set; }

    public string Address { get; set; }

    public string Status { get; set; }
    
    public string? OrganizationName { get; set; }

    public string? ContactPersonName { get; set; } 
    
    public List<FunderDocumentDto> FunderDocuments { get; set; }
}