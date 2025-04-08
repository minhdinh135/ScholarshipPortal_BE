using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class ScholarshipProgram : BaseEntity
{
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(1024)]
    public string? ImageUrl { get; set; }
    
    [MaxLength(200)]
    public string Description { get; set; }
    
    [MaxLength(200)]
    public string EducationLevel { get; set; }
    
    public decimal ScholarshipAmount { get; set; }
    
    public int NumberOfAwardMilestones { get; set; }
    
    public int NumberOfScholarships { get; set; }
    
    public DateTime Deadline { get; set; }
    
    [MaxLength(100)]
    public string Status { get; set; }
    
    public int FunderId { get; set; }
    
    public Account Funder { get; set; }
    
    public int CategoryId { get; set; }
    
    public Category Category { get; set; }
    
    public int UniversityId { get; set; }
    
    public University University { get; set; }
    
    public int MajorId { get; set; }
    
    public Major Major { get; set; }
    
    public ICollection<ExpertForProgram>? AssignedExperts { get; set; }
    
    public ICollection<Application>? Applications { get; set; }
    
    public ICollection<AwardMilestone> AwardMilestones { get; set; }
    
    public ICollection<ReviewMilestone> ReviewMilestones { get; set; }
    
    public ICollection<Criteria> Criteria { get; set; }
    
    public ICollection<Document> Documents { get; set; }
    
    public ICollection<ScholarshipProgramCertificate> ScholarshipProgramCertificates { get; set; }
}