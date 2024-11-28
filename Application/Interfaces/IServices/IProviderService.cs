using Domain.DTOs.Provider;

namespace Application.Interfaces.IServices;

public interface IProviderService
{
    Task<ProviderProfileDetails> GetProviderDetailsByProviderId(int providerId);
    Task<ProviderProfileDto> AddProviderDetails(AddProviderDetailsDto addProviderDetailsDto);
    Task<int> UpdateProviderDetails(int providerId, UpdateProviderDetailsDto updateProviderDetailsDto);
}