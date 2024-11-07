using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
{
    public FeedbackRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }

	public async Task<bool> FeedbackExists(int applicantId, int serviceId)
	{
		return await _dbContext.Feedbacks
			.AnyAsync(f => f.ApplicantId == applicantId && f.ServiceId == serviceId);
	}
}