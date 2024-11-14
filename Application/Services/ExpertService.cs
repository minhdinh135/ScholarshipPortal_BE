using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Expert;
using Domain.Entities;

namespace Application.Services;

public class ExpertService : IExpertService
{
    private readonly IMapper _mapper;
    private readonly IExpertRepository _expertRepository;

    public ExpertService(IMapper mapper, IExpertRepository expertRepository)
    {
        _mapper = mapper;
        _expertRepository = expertRepository;
    }

    public async Task<ExpertProfileDto> GetExpertProfileByExpertId(int expertId)
    {
        var expert = await _expertRepository.GetExpertDetailsByExpertId(expertId);
        if (expert == null)
            throw new ServiceException($"Expert profile with expertId:{expertId} is not found", new NotFoundException());

        return _mapper.Map<ExpertProfileDto>(expert);
    }

    public async Task<ExpertProfileDto> CreateExpertProfile(CreateExpertDetailsDto createExpertDetailsDto)
    {
        try
        {
            var expert = _mapper.Map<ExpertProfile>(createExpertDetailsDto);
            var createdExpert = await _expertRepository.Add(expert);

            return _mapper.Map<ExpertProfileDto>(createdExpert);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<ExpertProfileDto> UpdateExpertProfile(int expertId, UpdateExpertDetailsDto updateExpertDetailsDto)
    {
        try
        {
            var existingExpert = await _expertRepository.GetExpertDetailsByExpertId(expertId);
            if(existingExpert == null)
                throw new ServiceException($"Expert profile with expertId:{expertId} is not found", new NotFoundException());
            
            _mapper.Map(updateExpertDetailsDto, existingExpert);
            var updatedExpert = await _expertRepository.Update(existingExpert);

            return _mapper.Map<ExpertProfileDto>(updatedExpert);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }
}