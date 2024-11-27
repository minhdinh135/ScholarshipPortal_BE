using Domain.DTOs.Category;
using Domain.DTOs.Major;
using Domain.DTOs.University;

namespace Domain.DTOs.ScholarshipProgram;

public class ScholarshipProgramDto 
{
    public int? Id { get; set; }

    public string? Name { get; set; }

    public string? ImageUrl { get; set; }

    public string? Description { get; set; }

    public decimal? ScholarshipAmount { get; set; }

    public int? NumberOfScholarships { get; set; }
    
    public int? NumberOfAwardMilestones { get; set; }

    public DateTime? Deadline { get; set; }

    public string? Status { get; set; }

    public int? FunderId { get; set; }
    
    public CategoryDto? Category { get; set; }

    public UniversityDto? University { get; set; }

    public MajorDto? Major { get; set; }

    public List<CertificateDto>? Certificates { get; set; }
    
    public List<AwardMilestoneDetails> AwardMilestones { get; set; }
    
    public List<CriteriaDetails> Criteria { get; set; }
    
    public List<ReviewMilestoneDetails> ReviewMilestones { get; set; }
}