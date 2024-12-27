using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ExpertRepository : GenericRepository<ExpertProfile>, IExpertRepository
{
    public ExpertRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }

    public async Task<ExpertProfile> GetExpertDetailsByExpertId(int expertId)
    {
        var expert = await _dbContext.ExpertProfiles
            .AsNoTracking()
            .AsSplitQuery()
            .Include(e => e.Expert)
            .FirstOrDefaultAsync(e => e.ExpertId == expertId);

        return expert;
    }

    public async Task<IEnumerable<ExpertProfile>> GetExpertsByScholarshipProgramId(int scholarshipProgramId)
    {
        var programExperts = await _dbContext.ExpertForPrograms
            .AsNoTracking()
            .AsSplitQuery()
            .Where(programExpert => programExpert.ScholarshipProgramId == scholarshipProgramId)
            .Select(programExpert => programExpert.ExpertId)
            .ToListAsync();
        
        var experts = await _dbContext.ExpertProfiles
            .AsNoTracking()
            .AsSplitQuery()
            .Include(e => e.Expert)
            .Where(e => programExperts.Contains(e.ExpertId))
            .ToListAsync();

        return experts;
    }
}