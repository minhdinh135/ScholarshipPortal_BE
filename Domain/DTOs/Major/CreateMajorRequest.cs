using Domain.DTOs.Common;

namespace Domain.DTOs.Major;

public class CreateMajorRequest : BaseCreateRequest
{
    public string? Name { get; set; }

    public string? Description { get; set; }
}