using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
{
	public async Task<bool> FeedbackExists(int applicantId, int serviceId)
	{
		return await _dbContext.Feedbacks
			.AnyAsync(f => f.ApplicantId == applicantId && f.ServiceId == serviceId);
	}
}