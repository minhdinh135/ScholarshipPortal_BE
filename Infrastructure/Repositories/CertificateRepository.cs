using Application.Interfaces.IRepositories;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class CertificateRepository : GenericRepository<Certificate>, ICertificateRepository;