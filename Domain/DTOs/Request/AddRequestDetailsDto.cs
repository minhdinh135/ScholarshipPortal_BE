namespace Domain.DTOs.Request;

public class AddRequestDetailsDto
{
    public DateTime? ExpectedCompletionTime { get; set; }

    public string? ApplicationNotes { get; set; }

    public string? ScholarshipType { get; set; }

    public string? ApplicationFileUrl { get; set; }

    public int? ServiceId { get; set; }
}