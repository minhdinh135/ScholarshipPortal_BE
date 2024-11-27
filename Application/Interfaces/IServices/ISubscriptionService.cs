using Domain.DTOs.Subscription;

namespace Application.Interfaces.IServices;

public interface ISubscriptionService
{
    Task<IEnumerable<SubscriptionDto>> GetAllSubscriptions();
    Task<SubscriptionDto> GetSubscriptionById(int id);
	Task<SubscriptionDto> GetSubscriptionByProviderId(int providerId);
	Task<int> AddSubscription(AddSubscriptionDto addSubscriptionDto);
    Task UpdateSubscription(int id, UpdateSubscriptionDto updateSubscriptionDto);
}