using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Common;
using Domain.DTOs.ScholarshipProgram;
using Domain.Entities;

namespace Application.Services;

public class ScholarshipProgramService : IScholarshipProgramService
{
    private readonly IMapper _mapper;
    private readonly IScholarshipProgramRepository _scholarshipProgramRepository;
    private readonly ICategoryService _categoryService;

    public ScholarshipProgramService(IMapper mapper, IScholarshipProgramRepository scholarshipProgramRepository, ICategoryService categoryService)
    {
        _mapper = mapper;
        _scholarshipProgramRepository = scholarshipProgramRepository;
        _categoryService = categoryService;
    }
    
    public async Task<IEnumerable<ScholarshipProgramDto>> GetAllScholarshipPrograms()
    {
        // var allScholarshipPrograms = await _scholarshipProgramRepository.GetAll();
        var allScholarshipPrograms = await _scholarshipProgramRepository.GetAllScholarshipPrograms();

        return _mapper.Map<IEnumerable<ScholarshipProgramDto>>(allScholarshipPrograms);
    }

    public async Task<PaginatedList<ScholarshipProgramDto>> GetScholarshipPrograms(int pageIndex, int pageSize, string sortBy, string sortOrder)
    {
        var scholarshipPrograms =
            await _scholarshipProgramRepository.GetPaginatedList(pageIndex, pageSize, sortBy, sortOrder);

        return _mapper.Map<PaginatedList<ScholarshipProgramDto>>(scholarshipPrograms);
    }

    public async Task<ScholarshipProgramDto> GetScholarshipProgramById(int id)
    {
        var scholarshipProgram = await _scholarshipProgramRepository.GetById(id);

        return _mapper.Map<ScholarshipProgramDto>(scholarshipProgram);
    }

    public async Task<ScholarshipProgramDto> CreateScholarshipProgram(CreateScholarshipProgramRequest createScholarshipProgramRequest)
    {
        var scholarshipProgram = _mapper.Map<ScholarshipProgram>(createScholarshipProgramRequest);

        var createdScholarshipProgram = await _scholarshipProgramRepository.Add(scholarshipProgram);

        return _mapper.Map<ScholarshipProgramDto>(createdScholarshipProgram);
    }

    public async Task<ScholarshipProgramDto> UpdateScholarshipProgram(int id, UpdateScholarshipProgramRequest updateScholarshipProgramRequest)
    {
        var existingScholarshipProgram = await _scholarshipProgramRepository.GetScholarsipProgramById(id);

        await _scholarshipProgramRepository.ClearExistingCategories(existingScholarshipProgram);
        await _scholarshipProgramRepository.ClearExistingUniversities(existingScholarshipProgram);
        await _scholarshipProgramRepository.ClearExistingMajors(existingScholarshipProgram);

        _mapper.Map(updateScholarshipProgramRequest, existingScholarshipProgram);

        var updatedScholarshipProgram = await _scholarshipProgramRepository.Update(existingScholarshipProgram);

        return _mapper.Map<ScholarshipProgramDto>(updatedScholarshipProgram);
    }

    public async Task<ScholarshipProgramDto> DeleteScholarshipProgramById(int id)
    {
        var deletedScholarshipProgram = await _scholarshipProgramRepository.DeleteById(id);

        return _mapper.Map<ScholarshipProgramDto>(deletedScholarshipProgram);
    }
}