using Domain.DTOs.Achievement;
using Domain.Entities;

namespace Application.Interfaces.IServices
{
	public interface IAchievementsService
	{
		Task<IEnumerable<Achievement>> GetAll();
		Task<Achievement> Get(int id);
		Task<Achievement> Add(AchievementAddDTO dto);
		Task<Achievement> Update(AchievementUpdateDTO dto);
		Task<Achievement> Delete(int id);
	}
}
