namespace Domain.DTOs.Request;

public class ApplicantCreateRequestDto
{
    public string? Description { get; set; }

    public int ApplicantId { get; set; }
    
    public List<int> ServiceIds { get; set; }
    
    public List<string>? RequestFileUrls { get; set; }
}