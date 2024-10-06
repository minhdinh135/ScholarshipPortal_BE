using Domain.DTOs.Common;

namespace Domain.DTOs.Major;

public class MajorDto : BaseDto
{
    public int? Id { get; set; }
    
    public string? Name { get; set; }

    public string? Description { get; set; }
}