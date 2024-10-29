using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
{
    public FeedbackRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }
}