using Domain.DTOs.Service;

namespace Domain.DTOs.Request;

public class RequestDetailsDto
{
    public int? Id { get; set; }
    
    public DateTime? ExpectedCompletionTime { get; set; }

    public string? ApplicationNotes { get; set; }

    public string? ScholarshipType { get; set; }

    public string? ApplicationFileUrl { get; set; }

    public int? RequestId { get; set; }
    
    public int? ServiceId { get; set; }
    public ServiceDto? Service { get; set; }
}