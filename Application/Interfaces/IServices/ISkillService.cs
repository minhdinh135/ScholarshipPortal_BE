using Domain.DTOs.Major;

namespace Application.Interfaces.IServices;

public interface ISkillService
{
    Task<IEnumerable<SkillDto>> GetAllSkills();
    Task<SkillDto> GetSkillById(int id);
}