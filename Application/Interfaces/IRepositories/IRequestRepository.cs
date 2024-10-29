using Domain.DTOs.Request;
using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IRequestRepository : IGenericRepository<Request>
{
    Task<IEnumerable<Request>> GetAllRequests();
    Task<Request> GetRequestById(int id);
	Task<bool> HasUserRequestedService(int serviceId, int applicantId);
}