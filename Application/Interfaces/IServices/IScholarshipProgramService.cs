﻿using Domain.DTOs.Common;
using Domain.DTOs.ScholarshipProgram;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.IServices;

public interface IScholarshipProgramService
{
    Task<PaginatedList<ScholarshipProgramDto>> GetAllPrograms(ListOptions listOptions);
    Task<IEnumerable<ScholarshipProgramDto>> GetAllScholarshipPrograms();
    Task<PaginatedList<ScholarshipProgramDto>> GetScholarshipPrograms(int pageIndex, int pageSize, string sortBy, string sortOrder);
    Task<IEnumerable<ScholarshipProgramDto>> GetScholarshipProgramsByFunderId(int funderId);
    Task<IEnumerable<ScholarshipProgramDto>> GetScholarshipProgramsByMajorId(int majorId);
    Task<ScholarshipProgramDto> GetScholarshipProgramById(int id);
    Task<int> CreateScholarshipProgram(CreateScholarshipProgramRequest createScholarshipProgramRequest);
    Task UpdateScholarshipProgram(int id, UpdateScholarshipProgramRequest updateScholarshipProgramRequest);
    Task UploadScholarshipProgramImage(int id, IFormFile file);
    Task<ScholarshipProgramDto> DeleteScholarshipProgramById(int id);
    Task<List<ScholarshipProgramElasticDocument>> SearchScholarships(ScholarshipSearchOptions scholarshipSearchOptions);
}
