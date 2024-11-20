using Domain.DTOs.AwardMilestone;
using Domain.DTOs.Common;

namespace Application.Interfaces.IServices
{
    public interface IAwardMilestoneService 
    {
        Task<IEnumerable<AwardMilestoneDto>> GetAll();
    
        Task<PaginatedList<AwardMilestoneDto>> GetAll(int pageIndex, int pageSize, string sortBy, string sortOrder);
        
        Task<AwardMilestoneDto> Get(int id);

        Task<IEnumerable<AwardMilestoneDto>> GetByScholarshipId(int scholarshipId);
        
        Task<AwardMilestoneDto> Add(CreateAwardMilestoneDto dto);
        
        Task<AwardMilestoneDto> Update(int id, UpdateAwardMilestoneDto dto);
        
        Task<AwardMilestoneDto> Delete(int id);
    }
}
