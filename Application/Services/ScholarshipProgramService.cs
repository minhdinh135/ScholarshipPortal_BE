﻿using Application.Exceptions;
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
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IElasticService<ScholarshipProgramElasticDocument> _scholarshipElasticService;
    private readonly IFunderService _funderService;

    public ScholarshipProgramService(IMapper mapper, IScholarshipProgramRepository scholarshipProgramRepository,
        ICloudinaryService cloudinaryService,
        IElasticService<ScholarshipProgramElasticDocument> scholarshipElasticService,
        IFunderService funderService)
    {
        _mapper = mapper;
        _scholarshipProgramRepository = scholarshipProgramRepository;
        _cloudinaryService = cloudinaryService;
        _scholarshipElasticService = scholarshipElasticService;
        _funderService = funderService;
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

    public async Task<PaginatedList<ScholarshipProgramDto>> GetScholarshipPrograms(int pageIndex, int pageSize,
        string sortBy, string sortOrder)
    {
        var scholarshipPrograms =
            await _scholarshipProgramRepository.GetPaginatedList(pageIndex, pageSize, sortBy, sortOrder);

        return _mapper.Map<PaginatedList<ScholarshipProgramDto>>(scholarshipPrograms);
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
        var funderExperts = await _funderService.GetExpertsByFunderId((int)scholarshipProgram.FunderId);
        var scholarshipExperts = funderExperts.Where(e => e.Major == scholarshipProgram.Major.Name);

        return scholarshipExperts;
    }

    public async Task<int> CreateScholarshipProgram(
        CreateScholarshipProgramRequest createScholarshipProgramRequest)
    {
        var scholarshipProgram = _mapper.Map<ScholarshipProgram>(createScholarshipProgramRequest);

        var createdScholarshipProgram = await _scholarshipProgramRepository.Add(scholarshipProgram);

        var existingScholarshipProgram =
            await _scholarshipProgramRepository.GetScholarsipProgramById(createdScholarshipProgram.Id);
        var scholarshipElasticDocument = _mapper.Map<ScholarshipProgramElasticDocument>(existingScholarshipProgram);
        await _scholarshipElasticService.AddOrUpdateScholarship(scholarshipElasticDocument);

        return createdScholarshipProgram.Id;
    }

    public async Task SeedElasticsearchData()
    {
        try
        {
            await _scholarshipElasticService.RemoveAllScholarships();
            var scholarshipPrograms = await _scholarshipProgramRepository.GetAllScholarshipPrograms();
            var scholarshipElasticDocuments =
                _mapper.Map<IEnumerable<ScholarshipProgramElasticDocument>>(scholarshipPrograms);
            await _scholarshipElasticService.AddOrUpdateBulkScholarship(scholarshipElasticDocuments);
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

            var scholarshipElasticDocument = _mapper.Map<ScholarshipProgramElasticDocument>(existingScholarshipProgram);
            await _scholarshipElasticService.AddOrUpdateScholarship(scholarshipElasticDocument);

            var updatedScholarshipProgram = await _scholarshipProgramRepository.Update(existingScholarshipProgram);

            return updatedScholarshipProgram.Id;
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task UpdateScholarshipProgramName(int id, string name)
    {
        var existingScholarshipProgram = await _scholarshipProgramRepository.GetScholarsipProgramById(id);
        if (existingScholarshipProgram == null)
            throw new NotFoundException($"Scholarship program with id:{id} is not found");

        try
        {
            existingScholarshipProgram.Name = name;
            var scholarshipElasticDocument = _mapper.Map<ScholarshipProgramElasticDocument>(existingScholarshipProgram);
            await _scholarshipElasticService.AddOrUpdate(scholarshipElasticDocument, "scholarships");

            await _scholarshipProgramRepository.Update(existingScholarshipProgram);
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
            var scholarshipElasticDocument = _mapper.Map<ScholarshipProgramElasticDocument>(existingScholarshipProgram);
            await _scholarshipElasticService.AddOrUpdate(scholarshipElasticDocument, "scholarships");

            await _scholarshipProgramRepository.Update(existingScholarshipProgram);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<ScholarshipProgramDto> DeleteScholarshipProgramById(int id)
    {
        var deletedScholarshipProgram = await _scholarshipProgramRepository.DeleteById(id);

        return _mapper.Map<ScholarshipProgramDto>(deletedScholarshipProgram);
    }

    public async Task<List<ScholarshipProgramElasticDocument>> SearchScholarships(
        ScholarshipSearchOptions scholarshipSearchOptions)
    {
        var scholarships = await _scholarshipElasticService.SearchScholarships(scholarshipSearchOptions);

        return scholarships;
    }

    public async Task<List<string>> SuggestScholarships(string input)
    {
        var suggestions = await _scholarshipElasticService.SuggestScholarships(input);

        return suggestions;
    }
}