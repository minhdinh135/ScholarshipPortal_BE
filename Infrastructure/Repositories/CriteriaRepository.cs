using Application.Interfaces.IRepositories;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class CriteriaRepository : GenericRepository<Criteria>, ICriteriaRepository;