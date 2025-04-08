using Application.Helper;
using Application.Interfaces.IRepositories;
using Domain.DTOs.Common;
using Microsoft.EntityFrameworkCore;
using Service = Domain.Entities.Service;

namespace Infrastructure.Repositories;

public class ServiceRepository : GenericRepository<Service>, IServiceRepository
{
    public async Task<PaginatedList<Service>> GetAllServicesByProviderId(int id, int pageIndex, int pageSize,
        string sortBy, string sortOrder)
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

        return new PaginatedList<Service>(items, totalCount, pageIndex, pageSize);
    }

    public async Task<PaginatedList<Service>> GetAllServices(ListOptions listOptions)
    {
        var query = _dbContext.Services
            .AsNoTracking()
            .AsSplitQuery()
            .Include(s => s.Feedbacks)
            .Include(s => s.RequestDetails)
            .OrderByDescending(s => s.UpdatedAt);

        if (!string.IsNullOrEmpty(listOptions.SortBy))
        {
            var orderByExpression = ExpressionUtils.GetOrderByExpression<Service>(listOptions.SortBy);
            query = listOptions.IsDescending
                ? query.OrderByDescending(orderByExpression)
                : query.OrderBy(orderByExpression);
        }

        var totalCount = await query.CountAsync();

        if (!listOptions.IsPaging)
        {
            var allItems = await query.ToListAsync();
            return new PaginatedList<Service>(allItems, totalCount, 1, totalCount);
        }

        var result = await query
            .Skip((listOptions.PageIndex - 1) * listOptions.PageSize)
            .Take(listOptions.PageSize)
            .ToListAsync();

        return new PaginatedList<Service>(result, totalCount, listOptions.PageIndex,
            listOptions.PageSize);
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

    public async Task<PaginatedList<Service>> GetAllActiveServices(int pageIndex, int pageSize, string sortBy,
        string sortOrder)
    {
        var query = _dbContext.Set<Service>().AsNoTracking().AsSplitQuery();

        if (!string.IsNullOrEmpty(sortBy))
        {
            var orderByExpression = ExpressionUtils.GetOrderByExpression<Service>(sortBy);
            query = sortOrder?.ToLower() == "desc"
                ? query.OrderByDescending(orderByExpression)
                : query.OrderBy(orderByExpression);
        }

        var items = await query
            .Where(x => x.Status == "Active")
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var totalCount = await _dbContext.Set<Service>().Where(x => x.Status == "Active").CountAsync();

        return new PaginatedList<Service>(items, totalCount, pageIndex, pageSize);
    }
}