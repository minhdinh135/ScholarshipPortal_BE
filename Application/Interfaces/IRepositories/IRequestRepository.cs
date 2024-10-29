using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IRequestRepository : IGenericRepository<Request>
{
	Task<bool> HasUserRequestedService(int serviceId, int applicantId);

}