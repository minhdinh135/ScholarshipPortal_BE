using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CertificateRepository : GenericRepository<Certificate>, ICertificateRepository
{
    public CertificateRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }
}