using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }

	public async Task<List<Transaction>> GetTransactionsByWalletSenderIdAsync(int walletSenderId)
	{
		return await _dbContext.Transactions
			.Where(t => t.WalletSenderId == walletSenderId)
			.ToListAsync();
	}

	public async Task<List<Transaction>> GetTransactionsByWalletUserIdAsync(int walletUserId)
	{
		return await _dbContext.Transactions
			.Where(t => t.WalletSenderId == walletUserId || t.WalletReceiverId == walletUserId)
			.ToListAsync();
	}

	public async Task<List<Transaction>> GetAllAsync()
	{
		return await _dbContext.Transactions.ToListAsync();
	}
}