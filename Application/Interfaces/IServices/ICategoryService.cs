using Domain.DTOs.Category;
using Domain.DTOs.Common;
using Domain.Entities;

namespace Application.Interfaces.IServices;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllCategories();
    Task<PaginatedList<CategoryDto>> GetCategories(int pageIndex, int pageSize, string sortBy, string sortOrder);
    Task<CategoryDto> GetCategoryById(int id);
    Task<CategoryDto> CreateCategory(CreateCategoryRequest createCategoryRequest);
    Task<CategoryDto> UpdateCategory(int id, UpdateCategoryRequest updateCategoryRequest);
    Task<CategoryDto> DeleteCategoryById(int id);
}