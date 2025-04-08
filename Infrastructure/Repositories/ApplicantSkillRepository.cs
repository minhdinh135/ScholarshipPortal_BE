using Application.Interfaces.IRepositories;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class ApplicantSkillRepository: GenericRepository<ApplicantSkill>, IApplicantSkillRepository;