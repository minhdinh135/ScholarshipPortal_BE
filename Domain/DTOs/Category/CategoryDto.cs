using Domain.DTOs.Common;

namespace Domain.DTOs.Category;

public class CategoryDto : BaseDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}