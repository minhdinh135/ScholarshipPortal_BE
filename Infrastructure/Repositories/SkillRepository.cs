using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class SkillRepository : GenericRepository<Skill>, ISkillRepository
{
    public SkillRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }
}