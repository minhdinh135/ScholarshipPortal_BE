namespace Domain.DTOs.Request;

public class AddRequestDto
{
    public string? Description { get; set; }

    public DateTime? RequestDate { get; set; }

    public string? Status { get; set; }

    public int? ApplicantId { get; set; }
    
    public List<AddRequestDetailsDto> RequestDetails { get; set; }
}