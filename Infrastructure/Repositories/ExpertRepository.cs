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

	public async Task<List<ExpertProfile>> GetAllExpertDetailsByExpert()
	{
        var expert = await _dbContext.ExpertProfiles
            .AsSplitQuery()
            .Include(e => e.Expert)
            .ToListAsync();
		return expert;
	}

	public async Task<ExpertProfile> GetExpertDetailsByExpertId(int expertId)
    {
        var expert = await _dbContext.ExpertProfiles
            .AsSplitQuery()
            .Include(e => e.Expert)
            .FirstOrDefaultAsync(e => e.ExpertId == expertId);

        return expert;
    }
}