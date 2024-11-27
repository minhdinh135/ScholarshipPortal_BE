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

    public SubscriptionService(IMapper mapper, ISubscriptionRepository subscriptionRepository)
    {
        _mapper = mapper;
        _subscriptionRepository = subscriptionRepository;
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
}