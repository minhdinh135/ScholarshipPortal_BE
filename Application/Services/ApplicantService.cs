using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Applicant;
using Domain.Entities;
using ServiceException = Application.Exceptions.ServiceException;

namespace Application.Services;

public class ApplicantService : IApplicantService
{
    private readonly IMapper _mapper;
    private readonly IApplicantRepository _applicantRepository;
    private readonly IPdfService _pdfService;
    private readonly IAccountRepository _accountRepository;
    private readonly IExperienceRepository _experienceRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IApplicantSkillRepository _applicantSkillRepository;
    private readonly IApplicantCertificateRepository _applicantCertificateRepository;

    public ApplicantService(IMapper mapper, IApplicantRepository applicantRepository, IPdfService pdfService,
        IAccountRepository accountRepository, IExperienceRepository experienceRepository,
        IEducationRepository educationRepository,
        IApplicantSkillRepository applicantSkillRepository,
        IApplicantCertificateRepository applicantCertificateRepository)
    {
        _mapper = mapper;
        _applicantRepository = applicantRepository;
        _pdfService = pdfService;
        _accountRepository = accountRepository;
        _experienceRepository = experienceRepository;
        _educationRepository = educationRepository;
        _applicantSkillRepository = applicantSkillRepository;
        _applicantCertificateRepository = applicantCertificateRepository;
    }

    public async Task<IEnumerable<ApplicantProfileDto>> GetAllApplicantProfiles()
    {
        var applicantProfiles = await _applicantRepository.GetAll();

        return _mapper.Map<IEnumerable<ApplicantProfileDto>>(applicantProfiles);
    }

    public async Task<ApplicantProfileDto> GetApplicantProfileDetails(int applicantId)
    {
        var applicantProfile = await _applicantRepository.GetByApplicantId(applicantId);
        if (applicantProfile == null)
            throw new ServiceException($"Applicant Profile with ApplicantId: {applicantId} is not found",
                new NotFoundException());

        return _mapper.Map<ApplicantProfileDto>(applicantProfile);
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

            List<ApplicantSkill> skills =
                updateDetails.Skills.Select(a => new ApplicantSkill { Name = a, Type = "" }).ToList();
            skills.ForEach(a => a.ApplicantProfileId = applicant.ApplicantProfile.Id);
            await _applicantRepository.UpdateProfileSkills(applicant.ApplicantProfile.Id, skills);

            List<ApplicantCertificate> certificates =
                updateDetails.Certificates.Select(a => new ApplicantCertificate { Name = a }).ToList();
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

    public async Task UpdateProfileGeneralInformation(int applicantId, UpdateApplicantGeneralInformationRequest request)
    {
        var account = await _accountRepository.GetById(applicantId);
        var applicantProfile = await _applicantRepository.GetByApplicantId(applicantId);
        if (applicantProfile == null || account == null)
            throw new ServiceException(MessageConstant.NOT_FOUND, new NotFoundException());

        account.AvatarUrl = request.AvatarUrl;
        _mapper.Map(request, applicantProfile);

        try
        {
            await _accountRepository.Update(account);
            await _applicantRepository.Update(applicantProfile);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task AddProfileExperience(int applicantId, AddExperienceRequest request)
    {
        var applicantProfile = await _applicantRepository.GetByApplicantId(applicantId);
        if (applicantProfile == null)
            throw new NotFoundException($"Applicant profile with applicantId:{applicantId} is not found");

        var experience = _mapper.Map<Experience>(request);
        experience.ApplicantProfileId = applicantProfile.Id;

        try
        {
            await _experienceRepository.Add(experience);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task UpdateProfileExperience(int applicantId, int experienceId, UpdateExperienceRequest request)
    {
        var applicantProfile = await _applicantRepository.GetByApplicantId(applicantId);
        if (applicantProfile == null)
            throw new NotFoundException($"Applicant profile with ApplicantId:{applicantId} is not found");

        var existingExperience = await _experienceRepository.GetById(experienceId);
        if (existingExperience == null)
            throw new NotFoundException($"Experience with ID: {experienceId} is not found");

        _mapper.Map(request, existingExperience);
        try
        {
            await _experienceRepository.Update(existingExperience);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task DeleteProfileExperience(int experienceId)
    {
        try
        {
            await _experienceRepository.DeleteById(experienceId);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task AddProfileEducation(int applicantId, AddEducationRequest request)
    {
        var applicantProfile = await _applicantRepository.GetByApplicantId(applicantId);
        if (applicantProfile == null)
            throw new NotFoundException($"Applicant profile with applicantId:{applicantId} is not found");

        var education = _mapper.Map<Education>(request);
        education.ApplicantProfileId = applicantProfile.Id;

        try
        {
            await _educationRepository.Add(education);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task UpdateProfileEducation(int applicantId, int educationId, UpdateEducationRequest request)
    {
        var applicantProfile = await _applicantRepository.GetByApplicantId(applicantId);
        if (applicantProfile == null)
            throw new NotFoundException($"Applicant profile with ApplicantId:{applicantId} is not found");

        var existingEducation = await _educationRepository.GetById(educationId);
        if (existingEducation == null)
            throw new NotFoundException($"Education with ID: {educationId} is not found");

        _mapper.Map(request, existingEducation);
        try
        {
            await _educationRepository.Update(existingEducation);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task DeleteProfileEducation(int educationId)
    {
        try
        {
            await _educationRepository.DeleteById(educationId);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task AddProfileSkill(int applicantId, AddApplicantSkillRequest request)
    {
        var applicantProfile = await _applicantRepository.GetByApplicantId(applicantId);
        if (applicantProfile == null)
            throw new NotFoundException($"Applicant profile with applicantId:{applicantId} is not found");

        var skill = _mapper.Map<ApplicantSkill>(request);
        skill.ApplicantProfileId = applicantProfile.Id;

        try
        {
            await _applicantSkillRepository.Add(skill);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task UpdateProfileSkill(int applicantId, int skillId, UpdateApplicantSkillRequest request)
    {
        var applicantProfile = await _applicantRepository.GetByApplicantId(applicantId);
        if (applicantProfile == null)
            throw new NotFoundException($"Applicant profile with ApplicantId:{applicantId} is not found");

        var existingSkill = await _applicantSkillRepository.GetById(skillId);
        if (existingSkill == null)
            throw new NotFoundException($"Applicant skill with ID: {skillId} is not found");

        _mapper.Map(request, existingSkill);
        try
        {
            await _applicantSkillRepository.Update(existingSkill);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task DeleteProfileSkill(int skillId)
    {
        try
        {
            await _applicantSkillRepository.DeleteById(skillId);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task AddProfileCertificate(int applicantId, AddApplicantCertificateRequest request)
    {
        var applicantProfile = await _applicantRepository.GetByApplicantId(applicantId);
        if (applicantProfile == null)
            throw new NotFoundException($"Applicant profile with applicantId:{applicantId} is not found");

        var certificate = _mapper.Map<ApplicantCertificate>(request);
        certificate.ApplicantProfileId = applicantProfile.Id;

        try
        {
            await _applicantCertificateRepository.Add(certificate);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task UpdateProfileCertificate(int applicantId, int certificateId,
        UpdateApplicantCertificateRequest request)
    {
        var applicantProfile = await _applicantRepository.GetByApplicantId(applicantId);
        if (applicantProfile == null)
            throw new NotFoundException($"Applicant profile with ApplicantId:{applicantId} is not found");

        var existingCertificate = await _applicantCertificateRepository.GetById(certificateId);
        if (existingCertificate == null)
            throw new NotFoundException($"Certificate with ID: {certificateId} is not found");

        _mapper.Map(request, existingCertificate);
        try
        {
            await _applicantCertificateRepository.Update(existingCertificate);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task DeleteProfileCertificate(int certificateId)
    {
        try
        {
            await _applicantCertificateRepository.DeleteById(certificateId);
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
            throw new NotFoundException($"Profile with ApplicantId: {applicantId} is not found");

        var pdf = await _pdfService.GenerateProfileInPdf(_mapper.Map<ApplicantProfileDto>(profile));

        return pdf;
    }
}