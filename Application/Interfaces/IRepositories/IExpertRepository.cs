using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IExpertRepository : IGenericRepository<ExpertProfile>
{
    Task<ExpertProfile> GetExpertDetailsByExpertId(int expertId);
    Task<List<ExpertProfile>> GetAllExpertDetailsByExpert();
	Task<List<ExpertProfile>> GetAllExpertDetailsByFunder(int funderId);
}