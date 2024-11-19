using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IWalletRepository : IGenericRepository<Wallet>
{
    Task<Wallet> GetWalletByUserId(int userId);
}