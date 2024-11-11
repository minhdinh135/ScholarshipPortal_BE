using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface ITransactionRepository : IGenericRepository<Transaction>
{
	Task<List<Transaction>> GetTransactionsByWalletSenderIdAsync(int walletSenderId);
}