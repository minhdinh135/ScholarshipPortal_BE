using Domain.DTOs.Common;

namespace Domain.DTOs.ScholarshipProgram;

public class UpdateScholarshipProgramRequest : BaseUpdateRequest
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? ScholarshipAmount { get; set; }

    public int? NumberOfScholarships { get; set; }

    public DateTime? Deadline { get; set; }

    public int? NumberOfRenewals { get; set; }

    public int? FunderId { get; set; }

    public int? ProviderId { get; set; }
}