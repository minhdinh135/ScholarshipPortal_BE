using Application.Helper;
using Domain.DTOs.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.IRepositories;

public interface IServiceRepository : IGenericRepository<Service>
{
    Task<PaginatedList<Service>> GetAllServices(ListOptions listOptions);
    Task<Service> GetServiceById(int id);
    Task<IEnumerable<Service>> GetServicesByProviderId(int providerId);

    Task<PaginatedList<Service>> GetAllServicesByProviderId(int id, int pageIndex, int pageSize, string sortBy, string sortOrder);
    Task<PaginatedList<Service>> GetAllActiveServices(int pageIndex, int pageSize, string sortBy, string sortOrder);
}