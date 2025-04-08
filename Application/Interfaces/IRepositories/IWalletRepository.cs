using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IWalletRepository : IGenericRepository<Wallet>
{
    Task<Wallet> GetWalletByUserId(int userId);
	Task<List<Wallet>> GetAll();
}