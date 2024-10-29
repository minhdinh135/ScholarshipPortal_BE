using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RequestRepository : GenericRepository<Request>, IRequestRepository
{
    public RequestRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }

	public async Task<bool> HasUserRequestedService(int serviceId, int applicantId)
	{
		return await _dbContext.Requests
			.Where(r => r.ApplicantId == applicantId)
			.Include(x => x.RequestDetails)
			.AnyAsync(r => r.RequestDetails.FirstOrDefault().ServiceId == serviceId);
	}
}