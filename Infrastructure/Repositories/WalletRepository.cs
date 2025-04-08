using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class WalletRepository : GenericRepository<Wallet>, IWalletRepository
{
    public async Task<Wallet> GetWalletByUserId(int userId)
    {
        var wallet = await _dbContext.Wallets
            .AsSplitQuery()
            .Include(w => w.Account)
            .FirstOrDefaultAsync(w => w.AccountId == userId);

        return wallet;
    }

	public async Task<List<Wallet>> GetAll()
	{
		return await _dbContext.Wallets.ToListAsync();
	}
}
