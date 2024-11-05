using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MajorRepository : GenericRepository<Major>, IMajorRepository
{
    public MajorRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Major>> GetAllMajors()
    {
        var majors = await _dbContext.Majors
            .AsNoTracking()
            .AsSplitQuery()
            .Include(m => m.MajorSkills)
            .ThenInclude(ms => ms.Skill)
            .ToListAsync();

        return majors;
    }

    public async Task<Major> GetMajorById(int id)
    {
        var major = await _dbContext.Majors
            .AsNoTracking()
            .AsSplitQuery()
            .Include(m => m.MajorSkills)
            .ThenInclude(ms => ms.Skill)
            .FirstOrDefaultAsync(m => m.Id == id);

        return major;
    }
}