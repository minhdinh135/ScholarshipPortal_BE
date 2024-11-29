using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProviderRepository : GenericRepository<ProviderProfile>, IProviderRepository
{
    public ProviderRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }


    public async Task<ProviderProfile> GetProviderDetailsByProviderId(int providerId)
    {
        var provider = await _dbContext.ProviderProfiles
            .AsSplitQuery()
            .Include(p => p.ProviderDocuments)
            .Include(p => p.Provider)
            .ThenInclude(p => p.Subscription)
            .FirstOrDefaultAsync(p => p.ProviderId == providerId);

        return provider;
    }

    public async Task UpdateProfileDocuments(int providerProfileId, List<ProviderDocument> documents)
    {
        await _dbContext.ProviderDocuments
            .Where(a => a.ProviderProfileId == providerProfileId)
            .ExecuteDeleteAsync();

        await _dbContext.ProviderDocuments
            .AddRangeAsync(documents);

        await _dbContext.SaveChangesAsync();
    }
}