namespace Domain.DTOs.Applicant;

public class ApplicantExperienceDto
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public string? Description { get; set; }

    public int FromYear { get; set; }

    public int ToYear { get; set; }
}