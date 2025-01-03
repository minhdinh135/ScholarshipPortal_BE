using Domain.DTOs.Common;
using Domain.DTOs.Expert;
using Domain.DTOs.ScholarshipProgram;

namespace Application.Interfaces.IServices;

public interface IScholarshipProgramService
{
    Task<PaginatedList<ScholarshipProgramDto>> GetAllPrograms(ListOptions listOptions);
    Task<IEnumerable<ScholarshipProgramDto>> GetAllScholarshipPrograms();
    Task<IEnumerable<ScholarshipProgramDto>> SearchScholarshipPrograms(
        ScholarshipSearchOptions scholarshipSearchOptions);

    Task<PaginatedList<ScholarshipProgramDto>> GetExpertAssignedPrograms(ListOptions listOptions, int expertId);
    Task<PaginatedList<ScholarshipProgramDto>> GetScholarshipProgramsByFunderId(ListOptions listOptions, int funderId);
    Task<IEnumerable<ScholarshipProgramDto>> GetScholarshipProgramsByMajorId(int majorId);
    Task<ScholarshipProgramDto> GetScholarshipProgramById(int id);
    Task<IEnumerable<ExpertDetailsDto>> GetScholarshipProgramExperts(int scholarshipProgramId);
    Task<int> CreateScholarshipProgram(CreateScholarshipProgramRequest createScholarshipProgramRequest);
    Task<int> UpdateScholarshipProgram(int id, UpdateScholarshipProgramRequest updateScholarshipProgramRequest);
    Task ChangeScholarshipProgramStatus(int id, ChangeScholarshipProgramStatusRequest request);
    Task AssignExpertsToScholarshipProgram(int scholarshipProgramId, List<int> expertIds);
    Task RemoveExpertsFromScholarshipProgram(int scholarshipProgramId, List<int> expertIds);
}
