using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Common;
using Domain.DTOs.Service;
using Domain.Entities;

namespace Application.Services;

public class ServiceService : IServiceService
{
    private readonly IMapper _mapper;
    private readonly IServiceRepository _serviceRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IEmailService _emailService;

    public ServiceService(IMapper mapper, IServiceRepository serviceRepository, IAccountRepository accountRepository,
        IEmailService emailService)
    {
        _mapper = mapper;
        _serviceRepository = serviceRepository;
        _accountRepository = accountRepository;
        _emailService = emailService;
    }

    public async Task<PaginatedList<ServiceDto>> GetAllServices(ListOptions listOptions)
    {
        var services = await _serviceRepository.GetAllServices(listOptions);

        return _mapper.Map<PaginatedList<ServiceDto>>(services);
    }

    public async Task<IEnumerable<ServiceDto>> GetAllServices()
    {
        var services = await _serviceRepository.GetAll();

        return _mapper.Map<IEnumerable<ServiceDto>>(services);
    }

	public async Task<PaginatedList<ServiceDto>> GetAll(int pageIndex, int pageSize, string sortBy, string sortOrder)
	{
		var services = await _serviceRepository.GetPaginatedList(pageIndex, pageSize, sortBy, sortOrder);
		return _mapper.Map<PaginatedList<ServiceDto>>(services);
	}

	public async Task<PaginatedList<ServiceDto>> GetAllByProviderId(int id, int pageIndex, int pageSize, string sortBy, string sortOrder)
	{
		var services = await _serviceRepository.GetAllServicesByProviderId(id, pageIndex, pageSize, sortBy, sortOrder);
		return _mapper.Map<PaginatedList<ServiceDto>>(services);
	}

	public async Task CheckSubscriptionEndDateProvider(int id)
	{
		var provider = await _accountRepository.GetAccountById(id);
		if (provider == null)
		{
			return;
		}

		var today = DateTime.Now;
		var subscriptionEndDate = provider.SubscriptionEndDate;

		if (subscriptionEndDate.HasValue && subscriptionEndDate.Value.Date == today.AddDays(7).Date)
		{
			await _emailService.SendEmailAsync(
				provider.Email,
				"Subscription Expiration Notice",
				"Your subscription will expire in 7 days, you can renew it."
			);
		}

		if (subscriptionEndDate.HasValue && subscriptionEndDate.Value.Date < today.Date)
		{
			provider.SubscriptionId = null;
			await _accountRepository.Update(provider);

			var providerServices = await _serviceRepository.GetServicesByProviderId(provider.Id);
			foreach (var service in providerServices)
			{
				service.Status = "Inactive";
				await _serviceRepository.Update(service);
			}
		}
	}



	public async Task<ServiceDto> GetServiceById(int id)
    {
        var service = await _serviceRepository.GetServiceById(id);
        if (service == null)
            throw new ServiceException($"Service with id:{id} not found", new NotFoundException());

        return _mapper.Map<ServiceDto>(service);
    }

	public async Task<IEnumerable<ServiceDto>> GetServicesByProviderId(int providerId)
	{
		// var services = await _serviceRepository.GetAll();
		// var providerServices = services.Where(service => service.ProviderId == providerId);

        var providerServices = await _serviceRepository.GetServicesByProviderId(providerId);

		return _mapper.Map<IEnumerable<ServiceDto>>(providerServices);
	}


	public async Task<ServiceDto> AddService(AddServiceDto addServiceDto)
    {
        var service = _mapper.Map<Service>(addServiceDto);
        var addedService = await _serviceRepository.Add(service);

        return _mapper.Map<ServiceDto>(addedService);
    }

    public async Task<ServiceDto> UpdateService(int id, UpdateServiceDto updateServiceDto)
    {
        var existingService = await _serviceRepository.GetById(id);
        if (existingService == null)
            throw new ServiceException($"Service with id:{id} not found", new NotFoundException());

        _mapper.Map(updateServiceDto, existingService);

        var updatedService = await _serviceRepository.Update(existingService);

        return _mapper.Map<ServiceDto>(updatedService);
    }
}
