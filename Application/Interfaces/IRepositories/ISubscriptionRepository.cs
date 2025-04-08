using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface ISubscriptionRepository : IGenericRepository<Subscription>
{
	Task<Subscription?> GetSubscriptionByProviderId(int providerId);
}