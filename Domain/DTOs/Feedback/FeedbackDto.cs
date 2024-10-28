namespace Domain.DTOs.Feedback;

public class FeedbackDto
{
    public int? Id { get; set; }
    
    public string? Content { get; set; }

    public double? Rating { get; set; }

    public DateTime? FeedbackDate { get; set; }

    public int? ApplicantId { get; set; }
    
    public int? ServiceId { get; set; }
}