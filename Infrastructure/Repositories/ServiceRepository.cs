using Application.Helper;
using Application.Interfaces.IRepositories;
using Domain.DTOs.Common;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Service = Domain.Entities.Service;

namespace Infrastructure.Repositories;

public class ServiceRepository : GenericRepository<Service>, IServiceRepository
{
    public ServiceRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }
	public async Task<PaginatedList<Service>> GetAllServicesByProviderId(int id, int pageIndex, int pageSize, string sortBy, string sortOrder)
	{
		var query = _dbContext.Set<Service>().AsNoTracking().AsSplitQuery();

		if (!string.IsNullOrEmpty(sortBy))
		{
			var orderByExpression = ExpressionUtils.GetOrderByExpression<Service>(sortBy);
			query = sortOrder.ToLower() == "desc"
				? query.OrderByDescending(orderByExpression)
				: query.OrderBy(orderByExpression);
		}

		var items = await query
            .Where(x => x.ProviderId == id)
			.Skip((pageIndex - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync();

		var totalCount = await _dbContext.Set<Service>().Where(x => x.ProviderId == id).CountAsync();
		var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

		return new PaginatedList<Service>(items, pageIndex, totalPages);
	}

	public async Task<PaginatedList<Service>> GetAllServices(ListOptions listOptions)
    {
        var includes = new Func<IQueryable<Service>, IQueryable<Service>>[]
        {
            q => q.Include(s => s.Feedbacks),
            q => q.Include(s => s.RequestDetails)
        };

        var services = await GetPaginatedList(includes, listOptions);
        
        return services;
    }

    public async Task<Service> GetServiceById(int id)
    {
        var service = await _dbContext.Services
            .AsSplitQuery()
            .Include(s => s.Feedbacks)
            .Include(s => s.RequestDetails)
            .ThenInclude(x => x.RequestDetailFiles)
            .FirstOrDefaultAsync(s => s.Id == id);

        return service;
    }

    public async Task<IEnumerable<Service>> GetServicesByProviderId(int providerId)
    {
        var services = await _dbContext.Services
            .AsNoTracking()
            .AsSplitQuery()
            .Include(s => s.Feedbacks)
            .Include(s => s.RequestDetails)
            .Where(s => s.ProviderId == providerId)
            .ToListAsync();

        return services;
    }
}