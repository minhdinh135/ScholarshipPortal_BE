using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IApplicantRepository : IGenericRepository<ApplicantProfile>
{
    Task<ApplicantProfile> GetByApplicantId(int applicantId);

    Task<List<int>> AddProfileAchievements(List<Achievement> achievements);

    Task UpdateProfileAchievements(int applicantProfileId, List<Achievement> achievements);
}