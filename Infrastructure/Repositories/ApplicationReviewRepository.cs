using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ApplicationReviewRepository : GenericRepository<ApplicationReview>, IApplicationReviewRepository
{
    public ApplicationReviewRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }
}