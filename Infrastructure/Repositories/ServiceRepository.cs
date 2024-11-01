using Application.Helper;
using Application.Interfaces.IRepositories;
using Domain.DTOs.Common;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ServiceRepository : GenericRepository<Service>, IServiceRepository
{
    public ServiceRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }

	public async Task<PaginatedList<Service>> GetPaginatedList(int pageIndex, int pageSize, string sortBy, string sortOrder)
	{
		var query = _dbContext.Set<Service>().AsNoTracking();

		if (!string.IsNullOrEmpty(sortBy))
		{
			var orderByExpression = ExpressionUtils.GetOrderByExpression<Service>(sortBy);
			query = sortOrder?.ToLower() == "desc"
				? query.OrderByDescending(orderByExpression)
				: query.OrderBy(orderByExpression);
		}

		var items = await query
			.Skip((pageIndex - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync();

		var totalCount = await query.CountAsync();
		var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

		return new PaginatedList<Service>(items, pageIndex, totalPages);
	}
}