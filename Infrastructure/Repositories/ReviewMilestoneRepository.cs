using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ReviewMilestoneRepository : GenericRepository<ReviewMilestone>, IReviewMilestoneRepository
{
    public ReviewMilestoneRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }
}