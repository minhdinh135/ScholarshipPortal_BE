using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ApplicantRepository : GenericRepository<ApplicantProfile>, IApplicantRepository
{
    public ApplicantRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }

    public async Task<ApplicantProfile> GetByApplicantId(int applicantId)
    {
        return await _dbContext.ApplicantProfiles
            .AsNoTracking()
            .AsSplitQuery()
            .Include(a => a.Applicant)
            .Include(a => a.Achievements)
            .Include(a => a.ApplicantSkills)
            .Include(a => a.ApplicantCertificates)
            .FirstOrDefaultAsync(a => a.ApplicantId == applicantId);
    }
}