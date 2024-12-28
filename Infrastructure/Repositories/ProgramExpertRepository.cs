using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ProgramExpertRepository: GenericRepository<ExpertForProgram>, IProgramExpertRepository;