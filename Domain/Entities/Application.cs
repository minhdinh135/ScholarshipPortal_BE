using System.Collections;

namespace Domain.Entities;

public class Application : BaseEntity
{
    public DateTime? AppliedDate { get; set; }
    
    public int? ApplicantId { get; set; }

    public Account? Applicant { get; set; }
    
    public int? ScholarshipProgramId { get; set; }
    
    public ScholarshipProgram? ScholarshipProgram { get; set; }
    
    public Award? Award { get; set; }
    
    public ICollection<Review>? Reviews { get; set; }
    
    public ICollection<Document>? Documents { get; set; }
}