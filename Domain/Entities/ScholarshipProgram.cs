namespace Domain.Entities;

public class ScholarshipProgram : BaseEntity
{
    public string? Name { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public string? Description { get; set; }
    
    public decimal? ScholarshipAmount { get; set; }
    
    public int? NumberOfScholarships { get; set; }
    
    public DateTime? Deadline { get; set; }
    
    public string? Status { get; set; }
    
    public int? FunderId { get; set; }
    
    public Account? Funder { get; set; }
    
    public int? CategoryId { get; set; }
    
    public Category? Category { get; set; }
    
    public ICollection<Application>? Applications { get; set; }
    
    public ICollection<AwardMilestone> AwardMilestones { get; set; }
    
    public ICollection<ReviewMilestone> ReviewMilestones { get; set; }
    
    public ICollection<Criteria>? Criteria { get; set; }
    
    public ICollection<ScholarshipProgramCertificate> ScholarshipProgramCertificates { get; set; }
    
    public ICollection<ScholarshipProgramUniversity>? ScholarshipProgramUniversities { get; set; }
    
    public ICollection<ScholarshipProgramMajor>? ScholarshipProgramMajors { get; set; }
}