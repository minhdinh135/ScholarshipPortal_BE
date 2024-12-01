namespace Domain.DTOs.Expert;

public class UpdateExpertDetailsDto
{
    public string Avatar { get; set; }

    public string Username { get; set; }

    public string Phone { get; set; }

    public string Address { get; set; }

    public string Status { get; set; }
    
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Major { get; set; }
}