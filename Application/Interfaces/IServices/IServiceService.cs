using Domain.DTOs.Service;

namespace Application.Interfaces.IServices;

public interface IServiceService
{
    Task<IEnumerable<ServiceDto>> GetAllServices();
    Task<ServiceDto> GetServiceById(int id);
    Task<ServiceDto> AddService(AddServiceDto addServiceDto);
    Task<ServiceDto> UpdateService(int id, UpdateServiceDto updateServiceDto);
	Task<IEnumerable<ServiceDto>> GetServicesByProviderId(int providerId);
}