using Domain.DTOs.Account;
using Domain.DTOs.ScholarshipProgram;

namespace Domain.DTOs.Application;

public class ApplicationFullDto
{
    public int Id { get; set; }

    public DateTime AppliedDate { get; set; }
    
    public string Status { get; set; }
    
    public int ApplicantId { get; set; }

    public AccountDto Applicant { get; set; }
    
    public int ScholarshipProgramId { get; set; }
    
    //public ScholarshipProgramDto ScholarshipProgram { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public List<ApplicationDocumentDto> ApplicationDocuments { get; set; }
    
    public List<ApplicationReviewDto> ApplicationReviews { get; set; }
}
