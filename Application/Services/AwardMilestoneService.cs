using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.AwardMilestone;
using Domain.DTOs.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class AwardMilestoneService : IAwardMilestoneService
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<AwardMilestone> _awardMilestoneRepository;

    public AwardMilestoneService(IMapper mapper, IGenericRepository<AwardMilestone> awardMilestoneRepositor)
    {
        _mapper = mapper;
        _awardMilestoneRepository = awardMilestoneRepositor;
    }

    public async Task<IEnumerable<AwardMilestoneDto>> GetAll()
    {
        var allCategories = await _awardMilestoneRepository.GetAll(q => q.Include(x => x.AwardMilestoneDocuments));

         return _mapper.Map<IEnumerable<AwardMilestoneDto>>(allCategories);
    }

    public async Task<PaginatedList<AwardMilestoneDto>> GetAll(int pageIndex, int pageSize, string sortBy, string sortOrder)
    {
        var categories = await _awardMilestoneRepository.GetPaginatedList(pageIndex, pageSize, sortBy, sortOrder);

        return _mapper.Map<PaginatedList<AwardMilestoneDto>>(categories);
    }

    public async Task<AwardMilestoneDto> Get(int id)
    {
        var category = await _awardMilestoneRepository.GetById(id);

        return _mapper.Map<AwardMilestoneDto>(category);
    }


    public async Task<IEnumerable<AwardMilestoneDto>> GetByScholarshipId(int scholarshipId)
    {
        var allCategories = await _awardMilestoneRepository.GetAll(q => q.Include(x => x.AwardMilestoneDocuments));

        allCategories = allCategories.Where(x => x.ScholarshipProgramId == scholarshipId);

        return _mapper.Map<IEnumerable<AwardMilestoneDto>>(allCategories);
    }

    public async Task<AwardMilestoneDto> Add(CreateAwardMilestoneDto dto)
    {
         var category = _mapper.Map<AwardMilestone>(dto);

        var createdCategory = await _awardMilestoneRepository.Add(category);

        return _mapper.Map<AwardMilestoneDto>(createdCategory);
    }

    public async Task<AwardMilestoneDto> Update(int id, UpdateAwardMilestoneDto dto)
    {
        var existingCategory = await _awardMilestoneRepository.GetById(id);

        _mapper.Map(dto, existingCategory);
        
        var updatedCategory = await _awardMilestoneRepository.Update(existingCategory);

        return _mapper.Map<AwardMilestoneDto>(updatedCategory);
    }

    public async Task<AwardMilestoneDto> Delete(int id)
    {
        var deletedCategory = await _awardMilestoneRepository.DeleteById(id);

        return _mapper.Map<AwardMilestoneDto>(deletedCategory);
    }
}
