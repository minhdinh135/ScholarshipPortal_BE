using Domain.DTOs.Category;
using Domain.DTOs.Common;
using Domain.DTOs.Major;
using Domain.DTOs.University;

namespace Domain.DTOs.ScholarshipProgram;

public class ScholarshipProgramDto : BaseDto
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

    public int? CategoryId { get; set; }

    public ICollection<UniversityDto>? Universities { get; set; }

    public ICollection<MajorDto>? Majors { get; set; }
}