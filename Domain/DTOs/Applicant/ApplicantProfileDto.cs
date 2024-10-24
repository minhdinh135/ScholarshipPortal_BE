using Domain.DTOs.Account;

namespace Domain.DTOs.Applicant;

public class ApplicantProfileDto
{
    public int Id { get; set; }
    
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? Gender { get; set; }

    public string? Nationality { get; set; }

    public string? Ethnicity { get; set; }

    public int? ApplicantId { get; set; }
    
    public AccountDto? Applicant { get; set; }
    
    public List<AchievementDto>? Achievements { get; set; }
}