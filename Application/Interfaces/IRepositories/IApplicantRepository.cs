using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IApplicantRepository : IGenericRepository<ApplicantProfile>
{
    Task<ApplicantProfile> GetByApplicantId(int applicantId);

    // Task<List<int>> AddProfileAchievements(List<Achievement> achievements);

    Task UpdateProfileAchievements(int applicantProfileId, List<Achievement> achievements);

    // Task<List<int>> AddProfileSkills(List<ApplicantSkill> skills);

    Task UpdateProfileSkills(int applicantProfileId, List<ApplicantSkill> skills);

	// Task<List<int>> AddProfileCertificates(List<ApplicantCertificate> certificates);

    Task UpdateProfileCertificates(int applicantProfileId, List<ApplicantCertificate> certificates);
}