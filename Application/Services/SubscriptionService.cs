using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Subscription;
using Domain.Entities;

namespace Application.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly IMapper _mapper;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IAccountRepository _accountRepository;

    public SubscriptionService(IMapper mapper, ISubscriptionRepository subscriptionRepository,
        IAccountRepository accountRepository)
    {
        _mapper = mapper;
        _subscriptionRepository = subscriptionRepository;
        _accountRepository = accountRepository;
    }

    public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptions()
    {
        var allSubscriptions = await _subscriptionRepository.GetAll();

        return _mapper.Map<IEnumerable<SubscriptionDto>>(allSubscriptions);
    }

    public async Task<SubscriptionDto> GetSubscriptionById(int id)
    {
        var subscription = await _subscriptionRepository.GetById(id);

        return _mapper.Map<SubscriptionDto>(subscription);
    }

    public async Task<int> AddSubscription(AddSubscriptionDto addSubscriptionDto)
    {
        var subscription = _mapper.Map<Subscription>(addSubscriptionDto);
        try
        {
            var addedSubscription = await _subscriptionRepository.Add(subscription);

            return addedSubscription.Id;
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task UpdateSubscription(int id, UpdateSubscriptionDto updateSubscriptionDto)
    {
        try
        {
            var existingSubscription = await _subscriptionRepository.GetById(id);
            if (existingSubscription == null)
                throw new ServiceException($"Subscription with ID: {id} is not found", new NotFoundException());

            _mapper.Map(updateSubscriptionDto, existingSubscription);
            await _subscriptionRepository.Update(existingSubscription);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

	public async Task<SubscriptionDto> GetSubscriptionByProviderId(int providerId)
	{
		var subscription = await _subscriptionRepository.GetSubscriptionByProviderId(providerId);
		if (subscription == null)
			throw new ServiceException($"No subscription found for provider with ID: {providerId}");

		return _mapper.Map<SubscriptionDto>(subscription);
	}

    public async Task<decimal> GetRevenue()
    {
        var subscriptions = await _subscriptionRepository.GetAll();
        var subscriptionBuy = await _accountRepository.GetAll();
        subscriptionBuy = subscriptionBuy.Where(x => 
            x.SubscriptionId != null).ToList();

        var total = 0m;
        foreach (var sub in subscriptionBuy)
        {
            var subscription = subscriptions
                .Where(x => x.Id == sub.SubscriptionId)
                .FirstOrDefault();
            total += subscription.Amount;
        }
        return total;
    }

    public async Task<object> GetSubscriptionSold(DateTime fromDate, 
        DateTime toDate)
    {
        var res = new List<object>();
        var monthRes = new List<string>();
        var months = new List<DateTime>();

        var current = new DateTime(fromDate.Year, fromDate.Month, 1); // Start at the beginning of the first month
        while (current <= toDate)
        {
            monthRes.Add(current.ToString("MMM")); // Add abbreviated month name
            months.Add(current);
            current = current.AddMonths(1); // Move to the next month
        }

        var subscriptions = await _subscriptionRepository.GetAll();
        var subscriptionBuy = await _accountRepository.GetAll();
        subscriptionBuy = subscriptionBuy.Where(x => 
            x.SubscriptionId != null).ToList();

        foreach (var subscription in subscriptions)
        {
            var monthBuy = new List<int>();

            var subscriptionBuyOfThisSub = subscriptionBuy
                .Where(x => x.SubscriptionId == subscription.Id)
                .ToList();
            foreach (var month in months)
            {
                var count = 0;
                foreach (var sub in subscriptionBuyOfThisSub)
                {
                    //Console.WriteLine(sub.SubscriptionEndDate.Value.AddMonths(-subscription.ValidMonths).Year);
                    //Console.WriteLine(month.Year);
                    //Console.WriteLine(sub.SubscriptionEndDate.Value.Month - subscription.ValidMonths);
                    if (sub.SubscriptionEndDate.Value.AddMonths(-subscription.ValidMonths).Year == month.Year &&
                    sub.SubscriptionEndDate.Value.AddMonths(- subscription.ValidMonths).Month == month.Month)
                    {
                        count++;
                    }
                }
                monthBuy.Add(count);
            }
            res.Add(new {
                Name = subscription.Name,
                Data = monthBuy
            });
        }
        return new {
            Months = monthRes,
            Data = res
        };
    }

}
