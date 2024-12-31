namespace Domain.DTOs.Applicant;

public class ApplicantSkillDto
{
    public int? Id { get; set; }
    
    public string? Name { get; set; }

    public string? Type { get; set; }
    
    public int FromYear { get; set; }
    
    public int ToYear { get; set; }

    public string? Description { get; set; }
}