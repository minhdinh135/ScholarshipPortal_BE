using Application.Interfaces.IRepositories;
using Domain.Constants;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ApplicationReviewRepository : GenericRepository<ApplicationReview>, IApplicationReviewRepository
{
    public ApplicationReviewRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<ApplicationReview>> GetApplicationReviewsResult(bool isFirstReview)
    {
        var applicationReviews = await _dbContext.ApplicationReviews
            .Where(review =>
                isFirstReview
                    ? review.Status == ApplicationReviewStatusEnum.Approved.ToString()
                    : review.Status == ApplicationReviewStatusEnum.Passed.ToString())
            .OrderByDescending(review => review.Score)
            .ToListAsync();

        return applicationReviews;
    }
}