using Application.Interfaces.IRepositories;
using Domain.DTOs.Request;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RequestRepository : GenericRepository<Request>, IRequestRepository
{
    public RequestRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Request>> GetAllRequests(RequestQueryParameters requestQueryParameters)
    {
        var query = _dbContext.Requests
            .AsNoTracking()
            .AsSplitQuery()
            .Include(r => r.RequestDetails)
            .ThenInclude(r => r.RequestDetailFiles)
            .Include(r => r.RequestDetails)
            .ThenInclude(r => r.Service)
            .AsQueryable();

        if (requestQueryParameters.ApplicantId.HasValue)
        {
            query = query.Where(r => r.ApplicantId == requestQueryParameters.ApplicantId);
        }

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Request>> GetRequestsByApplicantId(int applicantId)
    {
        var requests = await _dbContext.Requests
            .AsSplitQuery()
            .Include(r => r.RequestDetails)
            .ThenInclude(r => r.RequestDetailFiles)
            .Include(r => r.RequestDetails)
            .ThenInclude(r => r.Service)
            .Where(r => r.ApplicantId == applicantId)
            .ToListAsync();

        return requests;
    }

    public async Task<Request> GetRequestById(int id)
    {
        var request = await _dbContext.Requests
            .AsSplitQuery()
            .Include(r => r.RequestDetails)
            .ThenInclude(r => r.RequestDetailFiles)
            .Include(r => r.RequestDetails)
            .ThenInclude(r => r.Service)
            .FirstOrDefaultAsync(r => r.Id == id);

        return request;
    }

    public async Task<bool> HasUserRequestedService(int serviceId, int applicantId)
    {
        return await _dbContext.Requests
            .Where(r => r.ApplicantId == applicantId)
            .Include(x => x.RequestDetails)
            .AnyAsync(r => r.RequestDetails.FirstOrDefault().ServiceId == serviceId);
    }

    public async Task<bool> DeleteRequestAsync(int requestId)
    {
        var request = await _dbContext.Requests
            .Include(r => r.RequestDetails)
            .FirstOrDefaultAsync(r => r.Id == requestId);

        if (request != null)
        {
            _dbContext.RequestDetails.RemoveRange(request.RequestDetails);
            _dbContext.Requests.Remove(request);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }


    public async Task<IEnumerable<Request>> GetByServiceId(int serviceId)
    {
        var requests = await _dbContext.Requests
            .AsNoTracking()
            .Where(r => r.RequestDetails.Any(rd => rd.ServiceId == serviceId))
            .Include(r => r.Applicant)
            .Include(r => r.RequestDetails)
            .ThenInclude(rd => rd.Service)
            .ToListAsync();

        return requests;
    }

    public async Task<Request> GetWithApplicantAndRequestDetails(int id)
    {
        var request = await _dbContext.Requests
            .AsNoTracking()
            .Where(r => r.Id == id)
            .Include(r => r.Applicant)
            .ThenInclude(a => a.ApplicantProfile)
            .Include(r => r.RequestDetails)
            .ThenInclude(rd => rd.Service)
            .FirstOrDefaultAsync();

        return request;
    }
}