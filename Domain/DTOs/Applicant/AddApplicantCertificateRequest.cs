namespace Domain.DTOs.Applicant;

public class AddApplicantCertificateRequest
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public string? Url { get; set; }

    public int AchievedYear { get; set; }
}