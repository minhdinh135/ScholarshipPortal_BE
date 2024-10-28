using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IFunderRepository : IGenericRepository<FunderProfile>
{
    Task<FunderProfile> GetFunderDetailsByFunderId(int funderId);
}