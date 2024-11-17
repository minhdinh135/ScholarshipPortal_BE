namespace Domain.DTOs.ScholarshipProgram;

public class CreateScholarshipProgramRequest
{
    public string? Name { get; set; }

    public string? ImageUrl { get; set; }

    public string? Description { get; set; }

    public decimal? ScholarshipAmount { get; set; }

    public int? NumberOfScholarships { get; set; }

    public DateTime? Deadline { get; set; }

    public string? Status { get; set; }

    public int? FunderId { get; set; }

    public int? CategoryId { get; set; }
    
    public int? UniversityId { get; set; }

    public int? MajorId { get; set; }
    
    public List<int>? CertificateIds { get; set; }
}