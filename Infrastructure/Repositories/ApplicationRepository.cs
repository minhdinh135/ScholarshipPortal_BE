using Application.Interfaces.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ApplicationRepository : GenericRepository<Domain.Entities.Application>, IApplicationRepository
{
    public ApplicationRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }

    public async Task<Domain.Entities.Application> GetWithDocumentsAndAccount(int applicationId)
    {
        var application = await _dbContext.Applications
            .AsNoTracking()
            .Where(a => a.Id == applicationId)
            .Include(a => a.Applicant)
            .ThenInclude(a => a.ApplicantProfile)
            .Include(a => a.ApplicationDocuments)
            .FirstOrDefaultAsync();

        return application;
    }

    public async Task<IEnumerable<Domain.Entities.Application>> GetByScholarshipId(int scholarshipId)
    {
        var application = await _dbContext.Applications
            .AsNoTracking()
            .Where(a => a.ScholarshipProgramId == scholarshipId)
            .Include(a => a.Applicant)
            .ToListAsync();

        return application;

    }
}
