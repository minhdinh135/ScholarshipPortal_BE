using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Applicant;
using Domain.Entities;

namespace Application.Services;

public class ApplicantService : IApplicantService
{
    private readonly IMapper _mapper;
    private readonly IApplicantRepository _applicantRepository;
    private readonly IPdfService _pdfService;

    public ApplicantService(IMapper mapper, IApplicantRepository applicantRepository, IPdfService pdfService)
    {
        _mapper = mapper;
        _applicantRepository = applicantRepository;
        _pdfService = pdfService;
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
            throw new ServiceException($"Applicant Profile with applicantId:{applicantId} is not found");
        
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
            throw new RepositoryException("Add achievements failed");
        }
    }

    public Task<bool> UpdateProfileAchievements(int applicantId, List<UpdateAchievementDto> dtos)
    {
        throw new NotImplementedException();
    }

    public async Task<byte[]> ExportApplicantProfileToPdf(int applicantId)
    {
        var applicantProfile = await _applicantRepository.GetByApplicantId(applicantId);
        if (applicantProfile == null)
            throw new NotFoundException($"Applicant profile with applicantId: {applicantId} is not found");

        // var pdf =  _pdfService.ExportProfileToPdf(applicantProfile);
        var pdf = await _pdfService.GenerateProfileInPdf(_mapper.Map<ApplicantProfileDto>(applicantProfile));

        return pdf;
    }
}