using Domain.DTOs.Common;
using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IServiceRepository : IGenericRepository<Service>
{
	Task<PaginatedList<Service>> GetPaginatedList(int pageIndex, int pageSize, string sortBy, string sortOrder);

}