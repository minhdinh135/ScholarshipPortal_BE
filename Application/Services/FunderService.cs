using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Expert;
using Domain.DTOs.Funder;
using Domain.Entities;

namespace Application.Services;

public class FunderService : IFunderService
{
    private readonly IMapper _mapper;
    private readonly IFunderRepository _funderRepository;

    public FunderService(IMapper mapper, IFunderRepository funderRepository)
    {
        _mapper = mapper;
        _funderRepository = funderRepository;
    }
    
    public async Task<FunderProfileDto> GetFunderDetailsByFunderId(int funderId)
    {
        var funder = await _funderRepository.GetFunderDetailsByFunderId(funderId);

        return _mapper.Map<FunderProfileDto>(funder);
    }

    public async Task<FunderProfileDto> AddFunderDetails(AddFunderDetailsDto addFunderDetailsDto)
    {
        try
        {
            var addedProfile = _mapper.Map<FunderProfile>(addFunderDetailsDto);
            
            var addedFunderDetails = await _funderRepository.Add(addedProfile);

            return _mapper.Map<FunderProfileDto>(addedProfile);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<FunderProfileDto> UpdateFunderDetails(int funderId, UpdateFunderDetailsDto updateFunderDetailsDto)
    {
        var existingFunder = await _funderRepository.GetFunderDetailsByFunderId(funderId);
        if (existingFunder == null)
            throw new ServiceException($"Funder with funderId:{funderId} is not found", new NotFoundException());

        _mapper.Map(updateFunderDetailsDto, existingFunder);

        var updatedFunder = await _funderRepository.Update(existingFunder);

        return _mapper.Map<FunderProfileDto>(updatedFunder);
    }

    public async Task<IEnumerable<ExpertDetailsDto>> GetExpertsByFunderId(int id)
    {
        var experts = await _funderRepository.GetExpertsByFunderId(id);

        return _mapper.Map<IEnumerable<ExpertDetailsDto>>(experts);
    }
}