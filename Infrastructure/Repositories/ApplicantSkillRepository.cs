using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ApplicantSkillRepository: GenericRepository<ApplicantSkill>, IApplicantSkillRepository
{
    public ApplicantSkillRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }
}