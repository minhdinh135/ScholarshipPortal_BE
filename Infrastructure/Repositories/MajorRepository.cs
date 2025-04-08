using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MajorRepository : GenericRepository<Major>, IMajorRepository
{
    public async Task<IEnumerable<Major>> GetAllMajors()
    {
        var majors = await _dbContext.Majors
            .AsNoTracking()
            .AsSplitQuery()
            .Include(m => m.SubMajors)
            .Include(m => m.MajorSkills)
            .ThenInclude(ms => ms.Skill)
            .ToListAsync();

        return majors;
    }

    public async Task<IEnumerable<Major>> GetAllParentMajors()
    {
        var majors = await _dbContext.Majors
            .AsNoTracking()
            .AsSplitQuery()
            .Include(m => m.SubMajors)
            .Include(m => m.MajorSkills)
            .ThenInclude(ms => ms.Skill)
            .Where(m => m.ParentMajorId == null)
            .ToListAsync();

        return majors;
    }

    public async Task<IEnumerable<Major>> GetAllSubMajorsByParentMajor(int parentMajorId)
    {
        var majors = await _dbContext.Majors
            .AsNoTracking()
            .AsSplitQuery()
            .Include(m => m.SubMajors)
            .Include(m => m.MajorSkills)
            .ThenInclude(ms => ms.Skill)
            .Where(m => m.ParentMajorId == parentMajorId)
            .ToListAsync();

        return majors;
    }

    public async Task<Major> GetMajorById(int id)
    {
        var major = await _dbContext.Majors
            .AsNoTracking()
            .AsSplitQuery()
            .Include(m => m.SubMajors)
            .Include(m => m.MajorSkills)
            .ThenInclude(ms => ms.Skill)
            .FirstOrDefaultAsync(m => m.Id == id);

        return major;
    }
}