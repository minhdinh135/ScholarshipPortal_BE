using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Application : BaseEntity
{
    public DateTime AppliedDate { get; set; }
    
    [MaxLength(100)]
    public string Status { get; set; }
    
    public int ApplicantId { get; set; }

    public Account Applicant { get; set; }
    
    public int ScholarshipProgramId { get; set; }
    
    public ScholarshipProgram ScholarshipProgram { get; set; }
    
    public ICollection<ApplicationReview>? ApplicationReviews { get; set; }
    
    public ICollection<ApplicationDocument> ApplicationDocuments { get; set; }
}