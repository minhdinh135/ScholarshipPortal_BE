using Domain.DTOs.Provider;

namespace Application.Interfaces.IServices;

public interface IProviderService
{
	Task<List<ProviderProfileDetails>> GetAllProviderDetails();
    Task<ProviderProfileDetails> GetProviderDetailsByProviderId(int providerId);
    Task<ProviderProfileDto> AddProviderDetails(int providerId, AddProviderDetailsDto addProviderDetailsDto);
    Task<int> UpdateProviderDetails(int providerId, UpdateProviderDetailsDto updateProviderDetailsDto);
}