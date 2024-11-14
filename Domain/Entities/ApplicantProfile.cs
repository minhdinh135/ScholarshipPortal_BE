namespace Domain.Entities;

public class ApplicantProfile : BaseEntity
{
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public DateTime? BirthDate { get; set; }
    
    public string? Gender { get; set; }
    
    public string? Nationality { get; set; }
    
    public string? Ethnicity { get; set; }
    
    public int? ApplicantId { get; set; }
    
    public Account? Applicant { get; set; }
    
    public ICollection<Achievement>? Achievements { get; set; }
    
    public ICollection<ApplicantSkill>? ApplicantSkills { get; set; }
    
    public ICollection<ApplicantCertificate>? ApplicantCertificates { get; set; }
    
    public ICollection<Experience>? Experiences { get; set; }
}