using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Category;
using Domain.Entities;

namespace Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }
    public async Task<IEnumerable<CategoryDto>> GetAllCategories()
    {
        var allCategories = await _categoryRepository.GetAll();

         return _mapper.Map<IEnumerable<CategoryDto>>(allCategories);
    }

    public async Task<CategoryDto> GetCategoryById(int id)
    {
        var category = await _categoryRepository.GetById(id);

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> CreateCategory(CreateCategoryRequest createCategoryRequest)
    {
        var category = _mapper.Map<Category>(createCategoryRequest);

        var createdCategory = await _categoryRepository.Add(category);

        return _mapper.Map<CategoryDto>(createdCategory);
    }

    public async Task<CategoryDto> UpdateCategory(int id, UpdateCategoryRequest updateCategoryRequest)
    {
        var existingCategory = await _categoryRepository.GetById(id);

        _mapper.Map(updateCategoryRequest, existingCategory);
        
        var updatedCategory = await _categoryRepository.Update(existingCategory);

        return _mapper.Map<CategoryDto>(updatedCategory);
    }

    public async Task<CategoryDto> DeleteCategoryById(int id)
    {
        var deletedCategory = await _categoryRepository.DeleteById(id);

        return _mapper.Map<CategoryDto>(deletedCategory);
    }
}