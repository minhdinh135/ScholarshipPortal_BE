using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ServiceRepository : GenericRepository<Service>, IServiceRepository
{
    public ServiceRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Service>> GetServicesByProviderId(int providerId)
    {
        var services = await _dbContext.Services
            .AsNoTracking()
            .AsSplitQuery()
            .Where(s => s.ProviderId == providerId)
            .ToListAsync();

        return services;
    }
}