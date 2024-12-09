using Domain.DTOs.Applicant;

namespace Application.Interfaces.IServices;

public interface IApplicantService
{
    Task<IEnumerable<ApplicantProfileDto>> GetAllApplicantProfiles();
    Task<ApplicantProfileDto> GetApplicantProfile(int applicantId);
    Task<ApplicantProfileDetails> GetApplicantProfileDetails(int applicantId);
    Task<ApplicantProfileDto> AddApplicantProfile(int applicantId, AddApplicantProfileDto dto);
    Task<ApplicantProfileDto> UpdateApplicantProfile(int applicantId, UpdateApplicantProfileDto dto);
    Task<int> UpdateApplicantProfileDetails(int applicantId, UpdateApplicantProfileDetails updateDetails);
    Task UpdateProfileSkills(int applicantId, List<UpdateApplicantSkillDto> dtos);
    Task<byte[]> ExportApplicantProfileToPdf(int applicantId);
}