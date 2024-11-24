using Domain.DTOs.Expert;
using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IFunderRepository : IGenericRepository<FunderProfile>
{
    Task<FunderProfile> GetFunderDetailsByFunderId(int funderId);
    Task<IEnumerable<Account>> GetExpertsByFunderId(int funderId);
}