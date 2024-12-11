using Application.Interfaces.IRepositories;
using Domain.Constants;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ApplicationRepository : GenericRepository<Domain.Entities.Application>, IApplicationRepository
{
    private readonly IApplicationReviewRepository _applicationReviewRepository;

    public ApplicationRepository(ScholarshipContext dbContext, IApplicationReviewRepository applicationReviewRepository)
        : base(dbContext)
    {
        _applicationReviewRepository = applicationReviewRepository;
    }

    public async Task<Domain.Entities.Application> GetApplicationById(int id)
    {
        var application = await _dbContext.Applications
            .AsSplitQuery()
            .Include(a => a.ApplicationDocuments)
            .Include(a => a.ApplicationReviews)
            .FirstOrDefaultAsync(a => a.Id == id);

        return application;
    }

    public async Task<Domain.Entities.Application> GetWithDocumentsAndAccount(int applicationId)
    {
        var application = await _dbContext.Applications
            .AsSplitQuery()
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
            .AsSplitQuery()
            .Where(a => a.ScholarshipProgramId == scholarshipId)
            .Include(a => a.Applicant)
            .ToListAsync();

        return application;
    }

    public async Task<IEnumerable<Domain.Entities.Application>> GetByApplicantId(int applicantId)
    {
        var applications = await _dbContext.Applications
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
            .AsSplitQuery()
            .Where(a => a.ScholarshipProgramId == scholarshipProgramId)
            .Include(a => a.Applicant)
            .Include(a => a.ApplicationDocuments)
            .Include(a => a.ApplicationReviews)
            .ToListAsync();

        return applications;
    }

    public async Task<IEnumerable<Domain.Entities.Application>> GetExpertAssignedApplications(int expertId)
    {
        var expertAssignedReviews =
            await _applicationReviewRepository.GetAll(q => q.Where(ar =>
                ar.ExpertId == expertId));

        var expertAssignedReviewIds = expertAssignedReviews.Select(ar => ar.ApplicationId);

        var applications = await _dbContext.Applications
            .AsSplitQuery()
            .Include(a => a.ApplicationDocuments)
            .Include(a => a.ApplicationReviews)
            .Where(a => expertAssignedReviewIds.Any(id => a.Id == id))
            .ToListAsync();

        return applications;
    }

    public async Task<IEnumerable<ApplicationReview>> GetApplicationReviewsResult(int scholarshipProgramId,
        bool isFirstReview)
    {
        var applications = await _dbContext.Applications
            .AsSplitQuery()
            .Where(a => a.ScholarshipProgramId == scholarshipProgramId)
            .ToListAsync();

        var applicationIds = applications.Select(a => a.Id);

        var applicationReviews = await _dbContext
            .ApplicationReviews
            .AsSplitQuery()
            .Include(review => review.Application)
            .ThenInclude(application => application.Applicant)
            .ThenInclude(applicant => applicant.ApplicantProfile)
            .Where(review =>
                (isFirstReview
                    ? review.Status == ApplicationReviewStatusEnum.Approved.ToString()
                    : review.Status == ApplicationReviewStatusEnum.Passed.ToString()) &&
                      applicationIds.Contains(review.ApplicationId))
            .OrderByDescending(review => review.Score)
            .ToListAsync();

        return applicationReviews;
    }
}