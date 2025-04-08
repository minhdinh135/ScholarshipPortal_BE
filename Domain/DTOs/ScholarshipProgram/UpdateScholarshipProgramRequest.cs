using Domain.DTOs.Common;

namespace Domain.DTOs.ScholarshipProgram;

public class UpdateScholarshipProgramRequest
{
    public string? Name { get; set; }

    public string? ImageUrl { get; set; }

    public string? Description { get; set; }
    
    public string EducationLevel { get; set; }

    public decimal? ScholarshipAmount { get; set; }

    public int? NumberOfScholarships { get; set; }

    public int NumberOfAwardMilestones { get; set; }

    public DateTime? Deadline { get; set; }

    public string? Status { get; set; }

    public int? FunderId { get; set; }

    public int? CategoryId { get; set; }

    public int? UniversityId { get; set; }

    public int? MajorId { get; set; }

    public List<int>? CertificateIds { get; set; }

    public List<CriteriaDetails> Criteria { get; set; }

    public List<ReviewMilestoneDetails> ReviewMilestones { get; set; }
}