using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UniversityRepository : GenericRepository<University>, IUniversityRepository
{
    public async Task<IEnumerable<University>> GetAllUniversities()
    {
        var universities = await _dbContext.Universities
            .Include(u => u.Country)
            .ToListAsync();

        return universities;
    }

    public async Task<University> GetUniversityById(int id)
    {
        var university = await _dbContext.Universities
            .Include(u => u.Country)
            .FirstOrDefaultAsync(u => u.Id == id);

        return university;
    }
}