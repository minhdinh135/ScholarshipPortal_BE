using Application.Interfaces.IRepositories;
using Domain.DTOs.Category;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }

    // public async Task<IEnumerable<Category>> GetAll()
    // {
    //     return await GetAll();
    // }
    //
    // public Task<IEnumerable<Category>> GetAll()
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public async Task<Category> GetById(int id)
    // {
    //     return await Get(id);
    // }
    //
    // public async Task<Category> CreateCategory(Category category)
    // {
    //     var createdCategory = await Add(category);
    //
    //     return createdCategory;
    // }
    //
    // public async Task<Category> UpdateCategory(Category category)
    // {
    //     var updatedCategory = await Update(category);
    //
    //     return updatedCategory;
    // }
    //
    // public async Task<Category> DeleteById(int id)
    // {
    //     var deletedCategory = await Delete(id);
    //
    //     return deletedCategory;
    // }
}