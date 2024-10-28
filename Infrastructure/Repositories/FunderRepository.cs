using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FunderRepository : GenericRepository<FunderProfile>, IFunderRepository
{
    public FunderRepository(ScholarshipContext dbContext) : base(dbContext)
    {
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
}