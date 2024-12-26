using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ExperienceRepository: GenericRepository<Experience>, IExperienceRepository
{
    public ExperienceRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }
}