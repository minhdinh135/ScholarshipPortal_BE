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

    public ServiceService(IMapper mapper, IServiceRepository serviceRepository)
    {
        _mapper = mapper;
        _serviceRepository = serviceRepository;
    }

    public async Task<PaginatedList<ServiceDto>> GetAllServices(ListOptions listOptions)
    {
        var services = await _serviceRepository.GetAllServices(listOptions);

        return _mapper.Map<PaginatedList<ServiceDto>>(services);
    }

	public async Task<PaginatedList<ServiceDto>> GetAll(int pageIndex, int pageSize, string sortBy, string sortOrder)
	{
		var services = await _serviceRepository.GetPaginatedList(pageIndex, pageSize, sortBy, sortOrder);
		return _mapper.Map<PaginatedList<ServiceDto>>(services);
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