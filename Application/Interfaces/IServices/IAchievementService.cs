using Domain.DTOs.Applicant;
using Domain.DTOs.Common;

namespace Application.Interfaces.IServices
{
    public interface IAchievementService
    {
        Task<IEnumerable<AchievementDto>> GetAll();
        Task<PaginatedList<AchievementDto>> GetAll(int pageIndex, int pageSize, string sortBy, string sortOrder);
        Task<AchievementDto> Get(int id);
        Task<AchievementDto> Add(AddAchievementDto dto);
        Task<AchievementDto> Update(UpdateAchievementDto dto);
        Task<AchievementDto> Delete(int id);
    }
}