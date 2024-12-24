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
            .AsSplitQuery()
            .Include(a => a.Applicant)
            .Include(a => a.Experiences)
            .Include(a => a.ApplicantSkills)
            .Include(a => a.ApplicantCertificates)
            .FirstOrDefaultAsync(a => a.ApplicantId == applicantId);
    }

    public async Task UpdateProfileSkills(int applicantProfileId, List<ApplicantSkill> skills)
    {
        await _dbContext.ApplicantSkills
            .Where(a => a.ApplicantProfileId == applicantProfileId)
            .ExecuteDeleteAsync();

        await _dbContext.ApplicantSkills
            .AddRangeAsync(skills);

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateProfileCertificates(int applicantProfileId, List<ApplicantCertificate> certificates)
    {
        await _dbContext.ApplicantCertificates
            .Where(a => a.ApplicantProfileId == applicantProfileId)
            .ExecuteDeleteAsync();
        await _dbContext.ApplicantCertificates
            .AddRangeAsync(certificates);

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateProfileExperiences(int applicantProfileId, List<Experience> experiences)
    {
        await _dbContext.Experiences
            .Where(a => a.ApplicantProfileId == applicantProfileId)
            .ExecuteDeleteAsync();
        await _dbContext.Experiences
            .AddRangeAsync(experiences);

        await _dbContext.SaveChangesAsync();
    }
}