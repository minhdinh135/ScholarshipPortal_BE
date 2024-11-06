using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IServiceRepository : IGenericRepository<Service>
{
    Task<IEnumerable<Service>> GetAllServices();
    Task<Service> GetServiceById(int id);
    Task<IEnumerable<Service>> GetServicesByProviderId(int providerId);
}