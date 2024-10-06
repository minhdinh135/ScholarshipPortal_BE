using Domain.DTOs.Common;

namespace Domain.DTOs.Major;

public class UpdateMajorRequest : BaseUpdateRequest
{
    public string? Name { get; set; }

    public string? Description { get; set; }
}