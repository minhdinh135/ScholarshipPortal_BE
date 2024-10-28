using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Funder;
using Domain.DTOs.Provider;
using Domain.Entities;

namespace Application.Services;

public class ProviderService : IProviderService
{
    private readonly IMapper _mapper;
    private readonly IProviderRepository _providerRepository;

    public ProviderService(IMapper mapper, IProviderRepository providerRepository)
    {
        _mapper = mapper;
        _providerRepository = providerRepository;
    }


    public async Task<ProviderProfileDto> GetProviderDetailsByProviderId(int providerId)
    {
        var provider = await _providerRepository.GetProviderDetailsByProviderId(providerId);

        return _mapper.Map<ProviderProfileDto>(provider);
    }

    public async Task<ProviderProfileDto> AddProviderDetails(AddProviderDetailsDto addProviderDetailsDto)
    {
        try
        {
            var addedProfile = _mapper.Map<ProviderProfile>(addProviderDetailsDto);

            var addedProviderDetails = await _providerRepository.Add(addedProfile);

            return _mapper.Map<ProviderProfileDto>(addedProfile);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<ProviderProfileDto> UpdateProviderDetails(int providerId,
        UpdateProviderDetailsDto updateProviderDetailsDto)
    {
        var existingProvider = await _providerRepository.GetProviderDetailsByProviderId(providerId);
        if (existingProvider == null)
            throw new ServiceException($"Funder with providerId:{providerId} is not found", new NotFoundException());

        _mapper.Map(updateProviderDetailsDto, existingProvider);

        var updatedProvider = await _providerRepository.Update(existingProvider);

        return _mapper.Map<ProviderProfileDto>(updatedProvider);
    }
}