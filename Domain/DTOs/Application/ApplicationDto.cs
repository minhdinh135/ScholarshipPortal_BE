using Domain.DTOs.Applicant;
using Domain.DTOs.ScholarshipProgram;

namespace Domain.DTOs.Application;

public class ApplicationDto
{
    public int Id { get; set; }

    public DateTime AppliedDate { get; set; }
    
    public string Status { get; set; }
    
    public int ApplicantId { get; set; }
    
    public string ApplicantName { get; set; }

    public string ScholarshipName { get; set; }

    public DateTime UpdatedAt { get; set; }
    
    public ScholarshipProgramDto ScholarshipProgram { get; set; }
    
    public ApplicantProfileDto ApplicantProfile { get; set; }
    
    public List<ApplicationDocumentDto> ApplicationDocuments { get; set; }
    
    public List<ApplicationReviewDto> ApplicationReviews { get; set; }
}
