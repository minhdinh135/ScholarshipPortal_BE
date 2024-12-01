using Application.Interfaces.IRepositories;
using Domain.DTOs.Expert;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FunderRepository : GenericRepository<FunderProfile>, IFunderRepository
{
    private readonly IAccountRepository _accountRepository;

    public FunderRepository(ScholarshipContext dbContext, IAccountRepository accountRepository) : base(dbContext)
    {
        _accountRepository = accountRepository;
    }

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

    public async Task<IEnumerable<Account>> GetExpertsByFunderId(int funderId)
    {
        var allAccounts = await _accountRepository.GetAll(
            q => q.Include(x => x.ExpertProfile));

        var experts = allAccounts.Where(a => a.FunderId == funderId);

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