using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Criteria;
using Domain.Entities;

namespace Application.Services;

public class CriteriaService : ICriteriaService
{
    private readonly IMapper _mapper;
    private readonly ICriteriaRepository _criteriaRepository;

    public CriteriaService(IMapper mapper, ICriteriaRepository criteriaRepository)
    {
        _mapper = mapper;
        _criteriaRepository = criteriaRepository;
    }
    
    public async Task<IEnumerable<CriteriaDto>> GetAllCriteria()
    {
        var allCriteria = await _criteriaRepository.GetAll();

        return _mapper.Map<IEnumerable<CriteriaDto>>(allCriteria);
    }

    public async Task<CriteriaDto> GetCriteriaById(int id)
    {
        var criteria = _criteriaRepository.GetById(id);

        return _mapper.Map<CriteriaDto>(criteria);
    }

    public async Task<CriteriaDto> CreateCriteria(CreateCriteriaRequest createCriteriaRequest)
    {
        var criteria = _mapper.Map<Criteria>(createCriteriaRequest);

        var createdCriteria = await _criteriaRepository.Add(criteria);

        return _mapper.Map<CriteriaDto>(createdCriteria);
    }

    public async Task<CriteriaDto> UpdateCriteria(int id, UpdateCriteriaRequest updateCriteriaRequest)
    {
        var existingCriteria = await _criteriaRepository.GetById(id);

        _mapper.Map(updateCriteriaRequest, existingCriteria);

        var updatedCriteria = await _criteriaRepository.Update(existingCriteria);

        return _mapper.Map<CriteriaDto>(updatedCriteria);
    }

    public async Task<CriteriaDto> DeleteCriteriaById(int id)
    {
        var deletedCriteria = await _criteriaRepository.DeleteById(id);

        return _mapper.Map<CriteriaDto>(deletedCriteria);
    }
}