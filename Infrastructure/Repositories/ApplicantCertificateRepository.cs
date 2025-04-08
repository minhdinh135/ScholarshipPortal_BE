using Application.Interfaces.IRepositories;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class ApplicantCertificateRepository: GenericRepository<ApplicantCertificate>, IApplicantCertificateRepository;