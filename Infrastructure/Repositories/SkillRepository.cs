using Application.Interfaces.IRepositories;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class SkillRepository : GenericRepository<Skill>, ISkillRepository;