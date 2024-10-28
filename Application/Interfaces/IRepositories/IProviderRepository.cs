using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IProviderRepository : IGenericRepository<ProviderProfile>
{
    Task<ProviderProfile> GetProviderDetailsByProviderId(int providerId);
}