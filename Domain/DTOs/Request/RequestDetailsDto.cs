namespace Domain.DTOs.Request;

public class RequestDetailsDto
{
    public string? Comment { get; set; }
    
    public int? ServiceId { get; set; }
    
    public List<RequestFile> Files { get; set; }
}