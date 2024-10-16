using Domain.DTOs.Common;
using Microsoft.AspNetCore.Http;

namespace Domain.DTOs.ScholarshipProgram;

public class CreateScholarshipProgramRequest : BaseCreateRequest
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }

    public decimal? ScholarshipAmount { get; set; }

    public int? NumberOfScholarships { get; set; }

    public DateTime? Deadline { get; set; }

    public int? NumberOfRenewals { get; set; }

    public int? FunderId { get; set; }

    public int? ProviderId { get; set; }
    
    public List<int>? CategoryIds { get; set; }
    
    public List<int>? UniversityIds { get; set; }
    
    public List<int>? MajorIds { get; set; }
}