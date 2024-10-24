using Domain.DTOs.Applicant;

namespace Application.Interfaces.IServices;

public interface IApplicantService
{
    Task<IEnumerable<ApplicantProfileDto>> GetAllApplicantProfiles();
    Task<ApplicantProfileDto> GetApplicantProfile(int applicantId);
    Task<ApplicantProfileDto> AddApplicantProfile(AddApplicantProfileDto dto);
    
    Task<ApplicantProfileDto> UpdateApplicantProfile(int applicantId, UpdateApplicantProfileDto dto);

    Task<List<int>> AddProfileAchievements(int applicantId, List<AddAchievementDto> dtos);

    Task<bool> UpdateProfileAchievements(int applicantId, List<UpdateAchievementDto> dtos);

    Task<byte[]> ExportApplicantProfileToPdf(int applicantId);
}
