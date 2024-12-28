using Domain.DTOs.Applicant;
using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IApplicantRepository : IGenericRepository<ApplicantProfile>
{
    Task<ApplicantProfile> GetByApplicantId(int applicantId);
    Task UpdateProfileSkills(int applicantProfileId, List<ApplicantSkill> skills);
    Task UpdateProfileCertificates(int applicantProfileId, List<ApplicantCertificate> certificates);
    Task UpdateProfileExperiences(int applicantProfileId, List<Experience> experiences);
    
}