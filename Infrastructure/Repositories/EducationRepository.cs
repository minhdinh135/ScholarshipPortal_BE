using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class EducationRepository: GenericRepository<Education>, IEducationRepository
{
    public EducationRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }
}