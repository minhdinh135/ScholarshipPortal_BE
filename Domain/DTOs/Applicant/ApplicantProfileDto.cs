namespace Domain.DTOs.Applicant;

public class ApplicantProfileDto
{
    public string? Avatar { get; set; }
    
    public string Username { get; set; }
    
    public string Email { get; set; }
    
    public string Phone { get; set; }
    
    public string Address { get; set; }
    public string FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? BirthDate { get; set; }

    public string Gender { get; set; }

    public string? Nationality { get; set; }

    public string? Ethnicity { get; set; }
    
    public string? Bio { get; set; }
    
    public int ApplicantId { get; set; }
    
    public List<ApplicantEducationDto> ApplicantEducations { get; set; }
    
    public List<ApplicantSkillDto>? ApplicantSkills { get; set; }
    
    public List<ApplicantCertificateDto>? ApplicantCertificates { get; set; }
    
    public List<ApplicantExperienceDto> ApplicantExperience { get; set; }
}