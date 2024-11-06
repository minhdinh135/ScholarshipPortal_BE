using Application.Interfaces.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Service = Domain.Entities.Service;

namespace Infrastructure.Repositories;

public class ServiceRepository : GenericRepository<Service>, IServiceRepository
{
    public ServiceRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Service>> GetAllServices()
    {
        var services = await _dbContext.Services
            .AsNoTracking()
            .AsSplitQuery()
            .Include(s => s.Feedbacks)
            .Include(s => s.RequestDetails)
            .ToListAsync();

        return services;
    }

    public async Task<Service> GetServiceById(int id)
    {
        var service = await _dbContext.Services
            .AsSplitQuery()
            .Include(s => s.Feedbacks)
            .Include(s => s.RequestDetails)
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