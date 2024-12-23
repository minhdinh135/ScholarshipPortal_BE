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
        Task<ApplicationDto> GetApplicationById(int id);
        Task<IEnumerable<ApplicationDto>> GetExpertAssignedApplications(int expertId);
        Task<Domain.Entities.Application> GetWithDocumentsAndAccount(int applicationId);
        Task<IEnumerable<Domain.Entities.Application>> GetByScholarshipId(int scholarshipId);
        Task<ApplicationDto> Add(AddApplicationDto dto);
        Task<ApplicationDto> Update(int id, UpdateApplicationStatusRequest dto);
        Task<ApplicationDto> Delete(int id);
        Task<IEnumerable<ApplicationReviewDto>> GetAllReviews();
        Task<IEnumerable<ApplicationReviewDto>> GetReviewsResult(int scholarshipProgramId, bool isFirstReview);
        Task AssignApplicationsToExpert(AssignApplicationsToExpertRequest request);
        Task UpdateReviewResult(UpdateReviewResultDto updateReviewResultDto);
        Task<Domain.Entities.Application> ExtendApplication(ExtendApplicationDto extendApplicationDto);
        Task CheckApplicationAward(Domain.Entities.Application profile);
    }
}
