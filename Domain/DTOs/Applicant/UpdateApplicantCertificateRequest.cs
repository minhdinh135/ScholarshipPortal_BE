namespace Domain.DTOs.Applicant;

public class UpdateApplicantCertificateRequest
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public string? Url { get; set; }

    public int AchievedYear { get; set; }
}