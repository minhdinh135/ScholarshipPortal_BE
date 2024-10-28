using Application.Interfaces.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ApplicationRepository : GenericRepository<Domain.Entities.Application>, IApplicationRepository
{
    public ApplicationRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Domain.Entities.Application>> GetByApplicantId(int applicantId)
    {
        var applications = await _dbContext.Applications
            .AsNoTracking()
            .AsSplitQuery()
            .Where(a => a.ApplicantId == applicantId)
            .Include(a => a.ApplicationDocuments)
            .Include(a => a.ApplicationReviews)
            .ToListAsync();

        return applications;

    }

    public async Task<IEnumerable<Domain.Entities.Application>> GetByScholarshipProgramId(int scholarshipProgramId)
    {
        var applications = await _dbContext.Applications
            .AsNoTracking()
            .AsSplitQuery()
            .Where(a => a.ScholarshipProgramId == scholarshipProgramId)
            .Include(a => a.ApplicationDocuments)
            .Include(a => a.ApplicationReviews)
            .ToListAsync();

        return applications;
    }
}