using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ApplicantCertificateRepository: GenericRepository<ApplicantCertificate>, IApplicantCertificateRepository
{
    public ApplicantCertificateRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }
}