using Domain.DTOs.Category;
using Domain.Entities;

namespace Application.Interfaces.IServices;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllCategories();
    Task<CategoryDto> GetCategoryById(int id);
    Task<CategoryDto> CreateCategory(CreateCategoryRequest createCategoryRequest);
    Task<CategoryDto> UpdateCategory(int id, UpdateCategoryRequest updateCategoryRequest);
    Task<CategoryDto> DeleteCategoryById(int id);
}