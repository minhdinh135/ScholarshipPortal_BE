using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IExpertRepository : IGenericRepository<ExpertProfile>
{
    Task<ExpertProfile> GetExpertDetailsByExpertId(int expertId);
    Task<IEnumerable<ExpertProfile>> GetExpertsByScholarshipProgramId(int scholarshipProgramId);
}