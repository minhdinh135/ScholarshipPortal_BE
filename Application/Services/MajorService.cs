using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Common;
using Domain.DTOs.Major;
using Domain.Entities;

namespace Application.Services;

public class MajorService : IMajorService
{
    private readonly IMapper _mapper;
    private readonly IMajorRepository _majorRepository;
    private readonly IGenericRepository<MajorSkill> _majorSkillRepository;

    public MajorService(IMapper mapper, IMajorRepository majorRepository,
        IGenericRepository<MajorSkill> majorSkillRepository)
    {
        _mapper = mapper;
        _majorRepository = majorRepository;
        _majorSkillRepository = majorSkillRepository;
    }

    public async Task<IEnumerable<MajorDto>> GetAllMajors()
    {
        var allMajors = await _majorRepository.GetAllMajors();

        return _mapper.Map<IEnumerable<MajorDto>>(allMajors);
    }

    public async Task<IEnumerable<MajorDto>> GetAllParentMajors()
    {
        var allParentMajors = await _majorRepository.GetAllParentMajors();

        return _mapper.Map<IEnumerable<MajorDto>>(allParentMajors);
    }

    public async Task<IEnumerable<MajorDto>> GetSubMajorsByParentMajor(int parentMajorId)
    {
        var allSubMajors = await _majorRepository.GetAllSubMajorsByParentMajor(parentMajorId);

        return _mapper.Map<IEnumerable<MajorDto>>(allSubMajors);
    }

    public async Task<PaginatedList<MajorDto>> GetMajors(int pageIndex, int pageSize, string sortBy, string sortOrder)
    {
        var majors = await _majorRepository.GetPaginatedList(pageIndex, pageSize, sortBy, sortOrder);

        return _mapper.Map<PaginatedList<MajorDto>>(majors);
    }

    public async Task<MajorDto> GetMajorById(int id)
    {
        var major = await _majorRepository.GetMajorById(id);

        return _mapper.Map<MajorDto>(major);
    }

    public async Task<MajorDto> CreateMajor(CreateMajorRequest createMajorRequest)
    {
        var major = _mapper.Map<Major>(createMajorRequest);

        var createdMajor = await _majorRepository.Add(major);

        return _mapper.Map<MajorDto>(createdMajor);
    }

    public async Task<MajorDto> UpdateMajor(int id, UpdateMajorRequest updateMajorRequest)
    {
        var existingMajor = await _majorRepository.GetById(id);

        _mapper.Map(updateMajorRequest, existingMajor);

        var updatedMajor = await _majorRepository.Update(existingMajor);

        return _mapper.Map<MajorDto>(updatedMajor);
    }

    public async Task<MajorDto> UpdateMajorSkills(int id, UpdateMajorSkillsRequest updateMajorRequest)
    {
        var existingMajorSkills = await _majorSkillRepository.GetAll();
        existingMajorSkills = existingMajorSkills.Where(s => s.MajorId == id).ToList();

        foreach (var majorSkill in existingMajorSkills)
        {
            await _majorSkillRepository.DeleteById(majorSkill.MajorId, majorSkill.SkillId);
        }

        foreach (var skillId in updateMajorRequest.SkillIds)
        {
            var newMajorSkill = new MajorSkill
            {
                SkillId = skillId,
                MajorId = id
            };
            await _majorSkillRepository.Add(newMajorSkill);
        }

        var updatedMajor = await _majorRepository.GetById(id);

        return _mapper.Map<MajorDto>(updatedMajor);
    }

    public async Task<MajorDto> DeleteMajorById(int id)
    {
        var deletedMajor = await _majorRepository.DeleteById(id);

        return _mapper.Map<MajorDto>(deletedMajor);
    }
}
