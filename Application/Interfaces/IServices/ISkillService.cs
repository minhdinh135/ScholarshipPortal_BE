using Domain.DTOs.Major;
using Domain.DTOs.ScholarshipProgram;

namespace Application.Interfaces.IServices;

public interface ISkillService
{
    Task<IEnumerable<SkillDto>> GetAllSkills();
    Task<SkillDto> GetSkillById(int id);
}