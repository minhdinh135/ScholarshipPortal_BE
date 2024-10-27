using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Common;
using Domain.DTOs.ScholarshipProgram;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class ScholarshipProgramService : IScholarshipProgramService
{
    private readonly IMapper _mapper;
    private readonly IScholarshipProgramRepository _scholarshipProgramRepository;
    private readonly ICloudinaryService _cloudinaryService;

    public ScholarshipProgramService(IMapper mapper, IScholarshipProgramRepository scholarshipProgramRepository,
        ICloudinaryService cloudinaryService)
    {
        _mapper = mapper;
        _scholarshipProgramRepository = scholarshipProgramRepository;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<IEnumerable<ScholarshipProgramDto>> GetAllScholarshipPrograms()
    {
        var allScholarshipPrograms = await _scholarshipProgramRepository.GetAllScholarshipPrograms();

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
        var scholarshipPrograms =
            await _scholarshipProgramRepository.GetAll(x => x.Include(x => x.ScholarshipProgramMajors));
        scholarshipPrograms =
            scholarshipPrograms.Where(scholarshipProgram => scholarshipProgram
                .ScholarshipProgramMajors
                .Any(x => x.MajorId == majorId));

        return _mapper.Map<IEnumerable<ScholarshipProgramDto>>(scholarshipPrograms);
    }

    public async Task<ScholarshipProgramDto> GetScholarshipProgramById(int id)
    {
        var scholarshipProgram = await _scholarshipProgramRepository.GetScholarsipProgramById(id);

        return _mapper.Map<ScholarshipProgramDto>(scholarshipProgram);
    }

    public async Task<ScholarshipProgramDto> CreateScholarshipProgram(
        CreateScholarshipProgramRequest createScholarshipProgramRequest)
    {
        var scholarshipProgram = _mapper.Map<ScholarshipProgram>(createScholarshipProgramRequest);

        var createdScholarshipProgram = await _scholarshipProgramRepository.Add(scholarshipProgram);

        return _mapper.Map<ScholarshipProgramDto>(createdScholarshipProgram);
    }

    public async Task<ScholarshipProgramDto> UpdateScholarshipProgram(int id,
        UpdateScholarshipProgramRequest updateScholarshipProgramRequest)
    {
        var existingScholarshipProgram = await _scholarshipProgramRepository.GetScholarsipProgramById(id);
        if (existingScholarshipProgram == null)
            throw new NotFoundException($"Scholarship program with id:{id} is not found");

        try
        {
            await _scholarshipProgramRepository.DeleteRelatedInformation(existingScholarshipProgram);
            
            _mapper.Map(updateScholarshipProgramRequest, existingScholarshipProgram);
            
            var updatedScholarshipProgram = await _scholarshipProgramRepository.Update(existingScholarshipProgram);

            return _mapper.Map<ScholarshipProgramDto>(updatedScholarshipProgram);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task UploadScholarshipProgramImage(int id, IFormFile file)
    {
        var existingScholarshipProgram = await _scholarshipProgramRepository.GetById(id);

        if (existingScholarshipProgram == null)
        {
            throw new Exception("No scholarship program found");
        }

        try
        {
            var imageUrl = await _cloudinaryService.UploadImage(file);

            if (string.IsNullOrEmpty(imageUrl))
            {
                throw new Exception("Error uploading to Cloudinary");
            }

            existingScholarshipProgram.ImageUrl = imageUrl;

            await _scholarshipProgramRepository.Update(existingScholarshipProgram);
        }
        catch (Exception e)
        {
            throw new Exception("Error uploading image to Cloudinary: " + e.Message);
        }
    }

    public async Task<ScholarshipProgramDto> DeleteScholarshipProgramById(int id)
    {
        var deletedScholarshipProgram = await _scholarshipProgramRepository.DeleteById(id);

        return _mapper.Map<ScholarshipProgramDto>(deletedScholarshipProgram);
    }
}