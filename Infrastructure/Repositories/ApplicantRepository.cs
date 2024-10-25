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

    public async Task<List<int>> AddProfileAchievements(List<Achievement> achievements)
    {
        await _dbContext.Achievements
            .AddRangeAsync(achievements);
        await _dbContext.SaveChangesAsync();

        return achievements.Select(a => a.Id).ToList();
    }

    public async Task UpdateProfileAchievements(int applicantProfileId, List<Achievement> achievements)
    {
        await _dbContext.Achievements
            .Where(a => a.ApplicantProfileId == applicantProfileId)
            .ExecuteDeleteAsync();
        await AddProfileAchievements(achievements);
    }
}