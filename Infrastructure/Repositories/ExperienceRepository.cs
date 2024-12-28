using Application.Interfaces.IRepositories;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class ExperienceRepository: GenericRepository<Experience>, IExperienceRepository;