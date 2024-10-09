using Domain.DTOs.Common;

namespace Domain.DTOs.Category;

public class CreateCategoryRequest : BaseCreateRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}