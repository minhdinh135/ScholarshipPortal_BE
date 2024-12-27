using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Common;
using Domain.DTOs.Expert;
using Domain.DTOs.ScholarshipProgram;
using Domain.Entities;

namespace Application.Services;

public class ScholarshipProgramService : IScholarshipProgramService
{
    private readonly IMapper _mapper;
    private readonly IScholarshipProgramRepository _scholarshipProgramRepository;
    private readonly IFunderService _funderService;
    private readonly IProgramExpertRepository _programExpertRepository;

    public ScholarshipProgramService(IMapper mapper, IScholarshipProgramRepository scholarshipProgramRepository,
        IFunderService funderService, IProgramExpertRepository programExpertRepository)
    {
        _mapper = mapper;
        _scholarshipProgramRepository = scholarshipProgramRepository;
        _funderService = funderService;
        _programExpertRepository = programExpertRepository;
    }

    public async Task<PaginatedList<ScholarshipProgramDto>> GetAllPrograms(ListOptions listOptions)
    {
        var allScholarshipPrograms = await _scholarshipProgramRepository.GetAllScholarshipPrograms(listOptions);

        return _mapper.Map<PaginatedList<ScholarshipProgramDto>>(allScholarshipPrograms);
    }

    public async Task<IEnumerable<ScholarshipProgramDto>> GetAllScholarshipPrograms()
    {
        var allScholarshipPrograms = await _scholarshipProgramRepository.GetAll();

        return _mapper.Map<IEnumerable<ScholarshipProgramDto>>(allScholarshipPrograms);
    }

    public async Task<IEnumerable<ScholarshipProgramDto>> SearchScholarshipPrograms(
        ScholarshipSearchOptions scholarshipSearchOptions)
    {
        try
        {
            var result = await _scholarshipProgramRepository.SearchScholarshipPrograms(scholarshipSearchOptions);

            return _mapper.Map<IEnumerable<ScholarshipProgramDto>>(result);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<IEnumerable<ScholarshipProgramDto>> GetScholarshipProgramsByFunderId(int funderId)
    {
        var scholarshipPrograms =
            await _scholarshipProgramRepository.GetAll();
        scholarshipPrograms =
            scholarshipPrograms.Where(scholarshipProgram => scholarshipProgram.FunderId == funderId);

        return _mapper.Map<IEnumerable<ScholarshipProgramDto>>(scholarshipPrograms);
    }


    public async Task<IEnumerable<ScholarshipProgramDto>> GetScholarshipProgramsByMajorId(int majorId)
    {
        var scholarshipPrograms = await _scholarshipProgramRepository.GetScholarshipProgramByMajorId(majorId);

        return _mapper.Map<IEnumerable<ScholarshipProgramDto>>(scholarshipPrograms);
    }

    public async Task<ScholarshipProgramDto> GetScholarshipProgramById(int id)
    {
        var scholarshipProgram = await _scholarshipProgramRepository.GetScholarsipProgramById(id);

        if (scholarshipProgram == null)
            throw new ServiceException($"Scholarship Program with id:{id} is not found", new NotFoundException());

        return _mapper.Map<ScholarshipProgramDto>(scholarshipProgram);
    }

    public async Task<IEnumerable<ExpertDetailsDto>> GetScholarshipProgramExperts(int scholarshipProgramId)
    {
        var scholarshipProgram = await _scholarshipProgramRepository.GetScholarsipProgramById(scholarshipProgramId);
        var funderExperts = await _funderService.GetExpertsByFunderId(scholarshipProgram.FunderId);
        var scholarshipExperts = funderExperts.Where(e => e.Major == scholarshipProgram.Major.Name);

        return scholarshipExperts;
    }

    public async Task<int> CreateScholarshipProgram(
        CreateScholarshipProgramRequest createScholarshipProgramRequest)
    {
        var scholarshipProgram = _mapper.Map<ScholarshipProgram>(createScholarshipProgramRequest);

        try
        {
            var createdScholarshipProgram = await _scholarshipProgramRepository.Add(scholarshipProgram);

            return createdScholarshipProgram.Id;
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<int> UpdateScholarshipProgram(int id,
        UpdateScholarshipProgramRequest updateScholarshipProgramRequest)
    {
        var existingScholarshipProgram = await _scholarshipProgramRepository.GetScholarsipProgramById(id);
        if (existingScholarshipProgram == null)
            throw new NotFoundException($"Scholarship program with id:{id} is not found");

        try
        {
            await _scholarshipProgramRepository.DeleteScholarshipCertificates(existingScholarshipProgram);

            _mapper.Map(updateScholarshipProgramRequest, existingScholarshipProgram);

            var updatedScholarshipProgram = await _scholarshipProgramRepository.Update(existingScholarshipProgram);

            return updatedScholarshipProgram.Id;
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task ChangeScholarshipProgramStatus(int id, ChangeScholarshipProgramStatusRequest request)
    {
        var existingProgram = await _scholarshipProgramRepository.GetById(id);
        if (existingProgram == null)
            throw new ServiceException($"Scholarship program with ID: {id} is not found", new NotFoundException());

        existingProgram.Status = request.Status;
        await _scholarshipProgramRepository.Update(existingProgram);
    }

    public async Task AssignExpertsToScholarshipProgram(int scholarshipProgramId, List<int> expertIds)
    {
        try
        {
            foreach (var expertId in expertIds)
            {
                ExpertForProgram expertForProgram = new ExpertForProgram
                {
                    ScholarshipProgramId = scholarshipProgramId,
                    ExpertId = expertId
                };
                await _programExpertRepository.Add(expertForProgram);
            }
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task UpdateScholarshipProgramStatus(int id, string status)
    {
        var existingScholarshipProgram = await _scholarshipProgramRepository.GetScholarsipProgramById(id);
        if (existingScholarshipProgram == null)
            throw new NotFoundException($"Scholarship program with id:{id} is not found");

        try
        {
            existingScholarshipProgram.Status = status;

            await _scholarshipProgramRepository.Update(existingScholarshipProgram);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }
}