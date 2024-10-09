using System.Collections;

namespace Domain.Entities;

public class ScholarshipProgram : BaseEntity
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public decimal? ScholarshipAmount { get; set; }
    
    public int? NumberOfScholarships { get; set; }
    
    public DateTime? Deadline { get; set; }
    
    public int? NumberOfRenewals { get; set; }
    
    public int? FunderId { get; set; }
    
    public Account? Funder { get; set; }
    
    public int? ProviderId { get; set; }
    
    public Account? Provider { get; set; }
    
    public ICollection<Application>? Applications { get; set; }
    
    public ICollection<Criteria>? Criteria { get; set; }
    
    public ICollection<ScholarshipProgramCategory>? ScholarshipProgramCategories { get; set; }
    
    public ICollection<ScholarshipProgramUniversity>? ScholarshipProgramUniversities { get; set; }
    
    public ICollection<ScholarshipProgramMajor>? ScholarshipProgramMajors { get; set; }
}