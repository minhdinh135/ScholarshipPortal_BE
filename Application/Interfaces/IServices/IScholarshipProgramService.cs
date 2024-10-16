using Domain.DTOs.Common;
using Domain.DTOs.ScholarshipProgram;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.IServices;

public interface IScholarshipProgramService
{
    Task<IEnumerable<ScholarshipProgramDto>> GetAllScholarshipPrograms();
    Task<PaginatedList<ScholarshipProgramDto>> GetScholarshipPrograms(int pageIndex, int pageSize, string sortBy, string sortOrder);
    Task<IEnumerable<ScholarshipProgramDto>> GetScholarshipProgramsByFunderId(int funderId);
    Task<ScholarshipProgramDto> GetScholarshipProgramById(int id);
    Task<ScholarshipProgramDto> CreateScholarshipProgram(CreateScholarshipProgramRequest createScholarshipProgramRequest);
    Task<ScholarshipProgramDto> UpdateScholarshipProgram(int id, UpdateScholarshipProgramRequest updateScholarshipProgramRequest);
    Task UploadScholarshipProgramImage(int id, IFormFile file);
    Task<ScholarshipProgramDto> DeleteScholarshipProgramById(int id);
}
