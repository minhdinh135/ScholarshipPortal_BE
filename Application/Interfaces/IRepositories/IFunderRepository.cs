using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IFunderRepository : IGenericRepository<FunderProfile>
{
    Task<FunderProfile> GetFunderDetailsByFunderId(int funderId);
	Task<List<FunderProfile>> GetAllFunderDetails();
    Task<IEnumerable<Account>> GetExpertsByFunderId(int funderId);
    Task UpdateProfileDocuments(int funderProfileId, List<FunderDocument> documents);
}