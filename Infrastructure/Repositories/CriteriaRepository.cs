using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CriteriaRepository : GenericRepository<Criteria>, ICriteriaRepository
{
    public CriteriaRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }
}