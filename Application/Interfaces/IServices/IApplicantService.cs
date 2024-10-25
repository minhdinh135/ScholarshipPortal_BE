using Domain.DTOs.Applicant;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.IServices;

public interface IApplicantService
{
    Task<IEnumerable<ApplicantProfileDto>> GetAllApplicantProfiles();
    Task<ApplicantProfileDto> GetApplicantProfile(int applicantId);
    Task<ApplicantProfileDto> AddApplicantProfile(AddApplicantProfileDto dto);

    Task<ApplicantProfileDto> UpdateApplicantProfile(int applicantId, UpdateApplicantProfileDto dto);

    Task<List<int>> AddProfileAchievements(int applicantId, List<AddAchievementDto> dtos);

    Task UpdateProfileAchievements(int applicantId, List<UpdateAchievementDto> dtos);

    Task<List<int>> AddProfileSkills(int applicantId, List<AddApplicantSkillDto> dtos);

    Task UpdateProfileSkills(int applicantId, List<UpdateApplicantSkillDto> dtos);

    Task<List<int>> AddProfileCertificates(int applicantId, List<AddApplicantCertificateDto> dtos);

    Task UpdateProfileCertificates(int applicantId, List<UpdateApplicantCertificateDto> dtos);

    Task<List<string>> UploadCertificateImages(IFormFileCollection certificateFiles);

    Task<byte[]> ExportApplicantProfileToPdf(int applicantId);
}