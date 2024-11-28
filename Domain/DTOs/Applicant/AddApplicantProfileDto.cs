namespace Domain.DTOs.Applicant;

public class AddApplicantProfileDto
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? Gender { get; set; }
    
    public string? Major { get; set; }
    
    public double? Gpa { get; set; }
    
    public string? School { get; set; } 
    
    public string? Nationality { get; set; }

    public string? Ethnicity { get; set; }

    public int? ApplicantId { get; set; }
}