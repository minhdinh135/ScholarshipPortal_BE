using Domain.DTOs.Achievement;
using Domain.DTOs.Common;
using Domain.Entities;

namespace Application.Interfaces.IServices
{
	public interface IAchievementService
	{
		Task<IEnumerable<AchievementDTO>> GetAll();
    Task<PaginatedList<AchievementDTO>> GetAll(int pageIndex, int pageSize, string sortBy, string sortOrder);
		Task<AchievementDTO> Get(int id);
		Task<AchievementDTO> Add(AchievementAddDTO dto);
		Task<AchievementDTO> Update(AchievementUpdateDTO dto);
		Task<AchievementDTO> Delete(int id);
	}
}
