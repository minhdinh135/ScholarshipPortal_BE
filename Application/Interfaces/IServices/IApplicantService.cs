using Domain.DTOs.Applicant;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.IServices;

public interface IApplicantService
{
    Task<IEnumerable<ApplicantProfileDto>> GetAllApplicantProfiles();
    Task<ApplicantProfileDto> GetApplicantProfile(int applicantId);
    Task<ApplicantProfileDto> AddApplicantProfile(AddApplicantProfileDto dto);
    Task<ApplicantProfileDto> UpdateApplicantProfile(int applicantId, UpdateApplicantProfileDto dto);
    Task UpdateProfileAchievements(int applicantId, List<UpdateAchievementDto> dtos);
    Task UpdateProfileSkills(int applicantId, List<UpdateApplicantSkillDto> dtos);
    Task UpdateProfileCertificates(int applicantId, List<UpdateApplicantCertificateDto> dtos);
    Task<List<string>> UploadCertificateImages(IFormFileCollection certificateFiles);
    Task<byte[]> ExportApplicantProfileToPdf(int applicantId);
}