using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class WalletRepository : GenericRepository<Wallet>, IWalletRepository
{
    public WalletRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }

    public async Task<Wallet> GetWalletByUserId(int userId)
    {
        var wallet = await _dbContext.Wallets
            .AsSplitQuery()
            .Include(w => w.Account)
            .FirstOrDefaultAsync(w => w.AccountId == userId);

        return wallet;
    }
}