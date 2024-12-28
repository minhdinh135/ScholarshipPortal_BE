using Domain.DTOs.Applicant;

namespace Application.Interfaces.IServices;

public interface IApplicantService
{
    Task<IEnumerable<ApplicantProfileDto>> GetAllApplicantProfiles();
    Task<ApplicantProfileDto> GetApplicantProfileDetails(int applicantId);
    Task<ApplicantProfileDto> AddApplicantProfile(int applicantId, AddApplicantProfileDto dto);
    Task<ApplicantProfileDto> UpdateApplicantProfile(int applicantId, UpdateApplicantProfileDto dto);
    Task<int> UpdateApplicantProfileDetails(int applicantId, UpdateApplicantProfileDetails updateDetails);
    Task AddProfileExperience(int applicantId, AddExperienceRequest request);
    Task UpdateProfileExperience(int applicantId, int experienceId, UpdateExperienceRequest request);
    Task AddProfileEducation(int applicantId, AddEducationRequest request);
    Task UpdateProfileEducation(int applicantId, int educationId, UpdateEducationRequest request);
    Task AddProfileSkill(int applicantId, AddApplicantSkillRequest request);
    Task UpdateProfileSkill(int applicantId, int skillId, UpdateApplicantSkillRequest request);
    Task AddProfileCertificate(int applicantId, AddApplicantCertificateRequest request);
    Task UpdateProfileCertificate(int applicantId, int certificateId, UpdateApplicantCertificateRequest request);
    Task<byte[]> ExportApplicantProfileToPdf(int applicantId);
}