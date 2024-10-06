using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class MajorRepository : GenericRepository<Major>, IMajorRepository
{
    public MajorRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }
}