using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface ITransactionRepository : IGenericRepository<Transaction>
{
	Task<List<Transaction>> GetTransactionsByWalletSenderIdAsync(int walletSenderId);
	Task<List<Transaction>> GetTransactionsByWalletUserIdAsync(int walletUserId);
	Task<List<Transaction>> GetAllAsync();
}