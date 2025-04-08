using Domain.DTOs.Service;

namespace Domain.DTOs.Request;

public class RequestDto
{
    public int? Id { get; set; }
    
    public string? Description { get; set; }

    public DateTime? RequestDate { get; set; }

    public string? Status { get; set; }

    public int? ApplicantId { get; set; }
    public ServiceDto Service { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public List<RequestDetailsDto> RequestDetails { get; set; }
}