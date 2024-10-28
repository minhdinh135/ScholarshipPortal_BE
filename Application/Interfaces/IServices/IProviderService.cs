using Domain.DTOs.Provider;

namespace Application.Interfaces.IServices;

public interface IProviderService
{
    Task<ProviderProfileDto> GetProviderDetailsByProviderId(int providerId);
    Task<ProviderProfileDto> AddProviderDetails(AddProviderDetailsDto addProviderDetailsDto);
    Task<ProviderProfileDto> UpdateProviderDetails(int providerId, UpdateProviderDetailsDto updateProviderDetailsDto);
}