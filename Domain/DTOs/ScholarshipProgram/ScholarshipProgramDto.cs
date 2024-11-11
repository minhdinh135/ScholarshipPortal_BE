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

    public DateTime? Deadline { get; set; }

    public string? Status { get; set; }

    public int? FunderId { get; set; }
    
    public CategoryDto? Category { get; set; }

    public List<UniversityDto>? Universities { get; set; }

    public List<MajorDto>? MajorSkills { get; set; }

    public List<CertificateDto>? Certificates { get; set; }
}