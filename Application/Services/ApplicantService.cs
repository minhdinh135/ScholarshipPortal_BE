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
    private readonly IAccountRepository _accountRepository;

    public ApplicantService(IMapper mapper, IApplicantRepository applicantRepository, IPdfService pdfService,
        ICloudinaryService cloudinaryService, IAccountRepository accountRepository)
    {
        _mapper = mapper;
        _applicantRepository = applicantRepository;
        _pdfService = pdfService;
        _cloudinaryService = cloudinaryService;
        _accountRepository = accountRepository;
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

    public async Task<ApplicantProfileDetails> GetApplicantProfileDetails(int applicantId)
    {
        var profile = await _applicantRepository.GetByApplicantId(applicantId);

        return _mapper.Map<ApplicantProfileDetails>(profile);
    }

    public async Task<ApplicantProfileDto> AddApplicantProfile(int applicantId, AddApplicantProfileDto dto)
    {
        var applicantProfile = _mapper.Map<ApplicantProfile>(dto);
        applicantProfile.ApplicantId = applicantId;
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

    public async Task<int> UpdateApplicantProfileDetails(int applicantId,
        UpdateApplicantProfileDetails updateDetails)
    {
        var applicant = await _accountRepository.GetAccountById(applicantId);
        if (applicant == null)
            throw new ServiceException($"Applicant with ID {applicantId} is not found", new NotFoundException());

        try
        {
            applicant.Username = updateDetails.Username;
            applicant.PhoneNumber = updateDetails.Phone;
            applicant.Address = updateDetails.Address;
            applicant.AvatarUrl = updateDetails.AvatarUrl;
            await _accountRepository.Update(applicant);

            var applicantProfile = await _applicantRepository.GetByApplicantId(applicantId);
            applicantProfile.FirstName = updateDetails.FirstName;
            applicantProfile.LastName = updateDetails.LastName;
            applicantProfile.BirthDate = updateDetails.Birthdate;
            applicantProfile.Gender = updateDetails.Gender;
            applicantProfile.Nationality = updateDetails.Nationality;
            applicantProfile.Ethnicity = updateDetails.Ethnicity;
            applicantProfile.Major = updateDetails.Major;
            applicantProfile.School = updateDetails.School;
            applicantProfile.Gpa = updateDetails.Gpa;

            List<Achievement> achievements =
                updateDetails.Achievements.Select(a => new Achievement { Name = a }).ToList();
            achievements.ForEach(a => a.ApplicantProfileId = applicant.ApplicantProfile.Id);
            await _applicantRepository.UpdateProfileAchievements(applicant.ApplicantProfile.Id, achievements);

            List<ApplicantSkill> skills =
                updateDetails.Skills.Select(a => new ApplicantSkill { Name = a, Type = ""}).ToList();
            skills.ForEach(a => a.ApplicantProfileId = applicant.ApplicantProfile.Id);
            await _applicantRepository.UpdateProfileSkills(applicant.ApplicantProfile.Id, skills);

            List<ApplicantCertificate> certificates =
                updateDetails.Certificates.Select(a => new ApplicantCertificate { Name = a, Type = ""}).ToList();
            certificates.ForEach(a => a.ApplicantProfileId = applicant.ApplicantProfile.Id);
            await _applicantRepository.UpdateProfileCertificates(applicant.ApplicantProfile.Id, certificates);

            List<Experience> experiences =
                updateDetails.Experience.Select(a => new Experience { Name = a }).ToList();
            experiences.ForEach(a => a.ApplicantProfileId = applicant.ApplicantProfile.Id);
            await _applicantRepository.UpdateProfileExperiences(applicant.ApplicantProfile.Id, experiences);

            return applicant.Id;
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
            throw new NotFoundException($"Applicant profile with applicantId:{applicantId} is not found");

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
        var profile = await _applicantRepository.GetByApplicantId(applicantId);
        if (profile == null)
            throw new NotFoundException($"Profile with applicantId: {applicantId} is not found");

        var pdf = await _pdfService.GenerateProfileInPdf(_mapper.Map<ApplicantProfileDetails>(profile));

        return pdf;
    }
}