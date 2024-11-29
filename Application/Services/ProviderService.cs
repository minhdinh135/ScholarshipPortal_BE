using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Provider;
using Domain.Entities;

namespace Application.Services;

public class ProviderService : IProviderService
{
    private readonly IMapper _mapper;
    private readonly IProviderRepository _providerRepository;
    private readonly IAccountRepository _accountRepository;

    public ProviderService(IMapper mapper, IProviderRepository providerRepository, IAccountRepository accountRepository)
    {
        _mapper = mapper;
        _providerRepository = providerRepository;
        _accountRepository = accountRepository;
    }


    public async Task<ProviderProfileDetails> GetProviderDetailsByProviderId(int providerId)
    {
        var provider = await _providerRepository.GetProviderDetailsByProviderId(providerId);

        return _mapper.Map<ProviderProfileDetails>(provider);
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

    public async Task<int> UpdateProviderDetails(int providerId,
        UpdateProviderDetailsDto updateDetails)
    {
        var existingProvider = await _accountRepository.GetAccountById(providerId);
        if (existingProvider == null)
            throw new ServiceException($"Funder with providerId:{providerId} is not found", new NotFoundException());

        try
        {
            existingProvider.Username = updateDetails.Username;
            existingProvider.PhoneNumber = updateDetails.Phone;
            existingProvider.Address = updateDetails.Address;
            existingProvider.AvatarUrl = updateDetails.Avatar;
            existingProvider.Status = updateDetails.Status;
            await _accountRepository.Update(existingProvider);

            var providerProfile = await _providerRepository.GetProviderDetailsByProviderId(providerId);
            providerProfile.OrganizationName = updateDetails.OrganizationName;
            providerProfile.ContactPersonName = updateDetails.ContactPersonName;

            List<ProviderDocument> providerDocuments = updateDetails.ProviderDocuments.Select(d => new ProviderDocument
            {
                Name = d.Name,
                Type = d.Type,
                FileUrl = d.FileUrl
            }).ToList();
            providerDocuments.ForEach(d => d.ProviderProfileId = providerProfile.Id);
            await _providerRepository.UpdateProfileDocuments(providerProfile.Id, providerDocuments);
            
            return existingProvider.Id;
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }
}