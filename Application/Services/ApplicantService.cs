using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Applicant;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using ServiceException = Application.Exceptions.ServiceException;

namespace Application.Services;

public class ApplicantService : IApplicantService
{
    private readonly IMapper _mapper;
    private readonly IApplicantRepository _applicantRepository;
    private readonly IPdfService _pdfService;
    private readonly ICloudinaryService _cloudinaryService;

    public ApplicantService(IMapper mapper, IApplicantRepository applicantRepository, IPdfService pdfService,
        ICloudinaryService cloudinaryService)
    {
        _mapper = mapper;
        _applicantRepository = applicantRepository;
        _pdfService = pdfService;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<IEnumerable<ApplicantProfileDto>> GetAllApplicantProfiles()
    {
        var applicantProfiles = await _applicantRepository.GetAll();

        return _mapper.Map<IEnumerable<ApplicantProfileDto>>(applicantProfiles);
    }

    public async Task<ApplicantProfileDto> GetApplicantProfile(int applicantId)
    {
        var applicantProfile = await _applicantRepository.GetByApplicantId(applicantId);

        if (applicantProfile == null)
            throw new NotFoundException($"Applicant Profile with applicantId: {applicantId} is not found");

        return _mapper.Map<ApplicantProfileDto>(applicantProfile);
    }

    public async Task<ApplicantProfileDto> AddApplicantProfile(AddApplicantProfileDto dto)
    {
        var applicantProfile = _mapper.Map<ApplicantProfile>(dto);
        var addedApplicantProfile = await _applicantRepository.Add(applicantProfile);

        return _mapper.Map<ApplicantProfileDto>(addedApplicantProfile);
    }

    public async Task<ApplicantProfileDto> UpdateApplicantProfile(int applicantId, UpdateApplicantProfileDto dto)
    {
        var existingApplicantProfile = await _applicantRepository.GetByApplicantId(applicantId);
        if (existingApplicantProfile == null)
            throw new NotFoundException($"Applicant profile with applicantId: {applicantId} is not found");

        _mapper.Map(dto, existingApplicantProfile);

        var updatedScholarshipProgram = await _applicantRepository.Update(existingApplicantProfile);

        return _mapper.Map<ApplicantProfileDto>(updatedScholarshipProgram);
    }

    public async Task<List<int>> AddProfileAchievements(int applicantId, List<AddAchievementDto> dtos)
    {
        var applicantProfile = await _applicantRepository.GetByApplicantId(applicantId);
        if (applicantProfile == null)
            throw new ServiceException($"Applicant profile with applicantId:{applicantId} is not found");

        var achievements = _mapper.Map<List<Achievement>>(dtos);

        foreach (var achievement in achievements)
        {
            achievement.ApplicantProfileId = applicantProfile.Id;
        }

        try
        {
            var resultIds = await _applicantRepository.AddProfileAchievements(achievements);

            return resultIds;
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task UpdateProfileAchievements(int applicantId, List<UpdateAchievementDto> dtos)
    {
        var applicantProfile = await _applicantRepository.GetByApplicantId(applicantId);
        if (applicantProfile == null)
            throw new NotFoundException($"Applicant profile with applicantId:{applicantId} s not found");

        var achievements = _mapper.Map<List<Achievement>>(dtos);

        achievements.ForEach(a => a.ApplicantProfileId = applicantProfile.Id);

        try
        {
            await _applicantRepository.UpdateProfileAchievements(applicantProfile.Id, achievements);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<List<int>> AddProfileSkills(int applicantId, List<AddApplicantSkillDto> dtos)
    {
        var applicantProfile = await _applicantRepository.GetByApplicantId(applicantId);
        if (applicantProfile == null)
            throw new ServiceException($"Applicant profile with applicantId:{applicantId} is not found");

        var skills = _mapper.Map<List<ApplicantSkill>>(dtos);

        foreach (var skill in skills)
        {
            skill.ApplicantProfileId = applicantProfile.Id;
        }

        try
        {
            var resultIds = await _applicantRepository.AddProfileSkills(skills);

            return resultIds;
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task UpdateProfileSkills(int applicantId, List<UpdateApplicantSkillDto> dtos)
    {
        var applicantProfile = await _applicantRepository.GetByApplicantId(applicantId);
        if (applicantProfile == null)
            throw new NotFoundException($"Applicant profile with applicantId:{applicantId} s not found");

        var skills = _mapper.Map<List<ApplicantSkill>>(dtos);

        skills.ForEach(a => a.ApplicantProfileId = applicantProfile.Id);

        try
        {
            await _applicantRepository.UpdateProfileSkills(applicantProfile.Id, skills);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

	public async Task<List<ApplicantSkillDto>> GetSkillsByApplicantId(int applicantId)
	{
		var skills = await _applicantRepository.GetSkillsByApplicantId(applicantId);
		return _mapper.Map<List<ApplicantSkillDto>>(skills);
	}

	public async Task<List<int>> AddProfileCertificates(int applicantId, List<AddApplicantCertificateDto> dtos)
    {
        var applicantProfile = await _applicantRepository.GetByApplicantId(applicantId);
        if (applicantProfile == null)
            throw new ServiceException($"Applicant profile with applicantId:{applicantId} is not found");

        var certificates = _mapper.Map<List<ApplicantCertificate>>(dtos);

        foreach (var certificate in certificates)
        {
            certificate.ApplicantProfileId = applicantProfile.Id;
        }

        try
        {
            var resultIds = await _applicantRepository.AddProfileCertificates(certificates);

            return resultIds;
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task UpdateProfileCertificates(int applicantId, List<UpdateApplicantCertificateDto> dtos)
    {
        var applicantProfile = await _applicantRepository.GetByApplicantId(applicantId);
        if (applicantProfile == null)
            throw new NotFoundException($"Applicant profile with applicantId:{applicantId} s not found");

        var certificates = _mapper.Map<List<ApplicantSkill>>(dtos);

        certificates.ForEach(a => a.ApplicantProfileId = applicantProfile.Id);

        try
        {
            await _applicantRepository.UpdateProfileSkills(applicantProfile.Id, certificates);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<List<string>> UploadCertificateImages(IFormFileCollection certificateFiles)
    {
        try
        {
            var certificateUrls = new List<string>();
            
            foreach (var certificateFile in certificateFiles)
            {
                var certificateUrl = await _cloudinaryService.UploadImage(certificateFile);
                certificateUrls.Add(certificateUrl);
            }

            return certificateUrls;
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task UpdateProfileAchievements(int applicantId, List<UpdateApplicantSkillDto> dtos)
    {
        var applicantProfile = await _applicantRepository.GetByApplicantId(applicantId);
        if (applicantProfile == null)
            throw new NotFoundException($"Applicant profile with applicantId:{applicantId} s not found");

        var skills = _mapper.Map<List<ApplicantSkill>>(dtos);

        skills.ForEach(a => a.ApplicantProfileId = applicantProfile.Id);

        try
        {
            await _applicantRepository.UpdateProfileSkills(applicantProfile.Id, skills);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<byte[]> ExportApplicantProfileToPdf(int applicantId)
    {
        var applicantProfile = await _applicantRepository.GetByApplicantId(applicantId);
        if (applicantProfile == null)
            throw new NotFoundException($"Applicant profile with applicantId: {applicantId} is not found");

        var pdf = await _pdfService.GenerateProfileInPdf(_mapper.Map<ApplicantProfileDto>(applicantProfile));

        return pdf;
    }
}