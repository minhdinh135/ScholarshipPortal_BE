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
    Task<byte[]> ExportApplicantProfileToPdf(int applicantId);
}