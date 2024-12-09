using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Major;

namespace Application.Services;

public class SkillService : ISkillService
{
    private readonly IMapper _mapper;
    private readonly ISkillRepository _skillRepository;

    public SkillService(IMapper mapper, ISkillRepository skillRepository)
    {
        _mapper = mapper;
        _skillRepository = skillRepository;
    }

    public async Task<IEnumerable<SkillDto>> GetAllSkills()
    {
        var skills = await _skillRepository.GetAll();

        return _mapper.Map<IEnumerable<SkillDto>>(skills);
    }

    public async Task<SkillDto> GetSkillById(int id)
    {
        var skill = await _skillRepository.GetById(id);

        return _mapper.Map<SkillDto>(skill);
    }
}