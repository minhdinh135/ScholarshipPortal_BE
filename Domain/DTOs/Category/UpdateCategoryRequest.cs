using Domain.DTOs.Common;

namespace Domain.DTOs.Category;

public class UpdateCategoryRequest : BaseUpdateRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}