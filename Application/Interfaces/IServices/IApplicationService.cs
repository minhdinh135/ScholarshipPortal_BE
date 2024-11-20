using Application.Interfaces.IRepositories;
using Domain.DTOs.Application;
using Domain.DTOs.Common;

namespace Application.Interfaces.IServices
{
    public interface IApplicationService 
    {
        Task<IEnumerable<ApplicationDto>> GetAll();
        Task<PaginatedList<ApplicationDto>> GetAll(int pageIndex, int pageSize, string sortBy, string sortOrder);
        Task<IEnumerable<ApplicationDto>> GetApplicationsByApplicantId(int applicantId);
        Task<IEnumerable<ApplicationDto>> GetApplicationsByScholarshipProgramId(int scholarshipProgramId);
        Task<ApplicationDto> Get(int id);
        Task<Domain.Entities.Application> GetWithDocumentsAndAccount(int applicationId);
        Task<IEnumerable<Domain.Entities.Application>> GetByScholarshipId(int scholarshipId);
        Task<ApplicationDto> Add(AddApplicationDto dto);
        Task<ApplicationDto> Update(int id, UpdateApplicationStatusRequest dto);
        Task<ApplicationDto> Delete(int id);
        Task<IEnumerable<ApplicationReviewDto>> GetAllReviews();
        Task AssignApplicationsToExpert(AssignApplicationsToExpertRequest request);
        Task UpdateReviewResult(UpdateReviewResultDto updateReviewResultDto);
        Task<Domain.Entities.Application> ExtendApplication(ExtendApplicationDto extendApplicationDto);
    }
}
