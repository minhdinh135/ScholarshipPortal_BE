using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ServiceRepository : GenericRepository<Service>, IServiceRepository
{
    public ServiceRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }
}