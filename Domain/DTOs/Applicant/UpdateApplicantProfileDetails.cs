namespace Domain.DTOs.Applicant;

public class UpdateApplicantProfileDetails
{
    public string? AvatarUrl { get; set; }
    
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Username { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? Gender { get; set; }

    public DateTime Birthdate { get; set; }

    public string? Nationality { get; set; }

    public string? Ethnicity { get; set; }
    
    public string? Major { get; set; }
    
    public double? Gpa { get; set; }
    
    public string? School { get; set; }

    public List<string>? Achievements { get; set; }
    
    public List<string>? Skills { get; set; }

    public List<string>? Experience { get; set; }

    public List<string>? Certificates { get; set; }
}