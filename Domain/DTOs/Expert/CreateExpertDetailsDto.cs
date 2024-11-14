namespace Domain.DTOs.Expert;

public class CreateExpertDetailsDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Major { get; set; }
    
    public int? ExpertId { get; set; }
}