using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FunderRepository : GenericRepository<FunderProfile>, IFunderRepository
{
    public async Task<List<FunderProfile>> GetAllFunderDetails()
    {
        return await _dbContext.FunderProfiles
            .AsSplitQuery()
            .Include(f => f.FunderDocuments)
            .Include(f => f.Funder)
            .ToListAsync();
    }

    public async Task<FunderProfile> GetFunderDetailsByFunderId(int funderId)
    {
        var funder = await _dbContext.FunderProfiles
            .AsSplitQuery()
            .Include(f => f.FunderDocuments)
            .Include(f => f.Funder)
            .FirstOrDefaultAsync(f => f.FunderId == funderId);

        return funder;
    }

    public async Task<IEnumerable<ExpertProfile>> GetExpertsByFunderId(int funderId)
    {
        var experts = await _dbContext.ExpertProfiles
            .AsNoTracking()
            .AsSplitQuery()
            .Include(expertProfile => expertProfile.Expert)
            .Where(expertProfile => expertProfile.Expert.FunderId == funderId)
            .ToListAsync();

        return experts;
    }

    public async Task UpdateProfileDocuments(int funderProfileId, List<FunderDocument> documents)
    {
        await _dbContext.FunderDocuments
            .Where(a => a.FunderProfileId == funderProfileId)
            .ExecuteDeleteAsync();

        await _dbContext.FunderDocuments
            .AddRangeAsync(documents);

        await _dbContext.SaveChangesAsync();
    }
}