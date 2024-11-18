using Domain.DTOs.Common;
using Domain.DTOs.Service;

namespace Application.Interfaces.IServices;

public interface IServiceService
{
    Task<PaginatedList<ServiceDto>> GetAllServices(ListOptions listOptions);
    Task<ServiceDto> GetServiceById(int id);
    Task<ServiceDto> AddService(AddServiceDto addServiceDto);
    Task<ServiceDto> UpdateService(int id, UpdateServiceDto updateServiceDto);
	Task<IEnumerable<ServiceDto>> GetServicesByProviderId(int providerId);
	Task<PaginatedList<ServiceDto>> GetAll(int pageIndex, int pageSize, string sortBy, string sortOrder);

}