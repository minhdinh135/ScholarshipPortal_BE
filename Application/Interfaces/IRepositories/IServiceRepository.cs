using Domain.DTOs.Common;
using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IServiceRepository : IGenericRepository<Service>
{
    Task<PaginatedList<Service>> GetAllServices(ListOptions listOptions);
    Task<Service> GetServiceById(int id);
    Task<IEnumerable<Service>> GetServicesByProviderId(int providerId);
}