using Application.Interfaces.IRepositories;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class EducationRepository: GenericRepository<Education>, IEducationRepository;