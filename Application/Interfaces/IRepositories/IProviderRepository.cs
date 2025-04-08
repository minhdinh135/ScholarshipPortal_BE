using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IProviderRepository : IGenericRepository<ProviderProfile>
{
    Task<ProviderProfile> GetProviderDetailsByProviderId(int providerId);
	Task<List<ProviderProfile>> GetAllProviderDetails();
	Task UpdateProfileDocuments(int providerProfileId, List<ProviderDocument> documents);
}