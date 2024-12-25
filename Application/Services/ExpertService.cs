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
    private readonly IAccountRepository _accountRepository;

    public ExpertService(IMapper mapper, IExpertRepository expertRepository, IAccountRepository accountRepository)
    {
        _mapper = mapper;
        _expertRepository = expertRepository;
        _accountRepository = accountRepository;
    }

	public async Task<List<ExpertDetailsDto>> GetAllExpertProfileByExpert()
	{
		var expert = await _expertRepository.GetAllExpertDetailsByExpert();
		return _mapper.Map<List<ExpertDetailsDto>>(expert);
	}

	public async Task<ExpertDetailsDto> GetExpertProfileByExpertId(int expertId)
    {
        var expert = await _expertRepository.GetExpertDetailsByExpertId(expertId);
        if (expert == null)
            throw new ServiceException($"Expert profile with expertId:{expertId} is not found",
                new NotFoundException());

        return _mapper.Map<ExpertDetailsDto>(expert);
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

    public async Task<int> UpdateExpertProfile(int expertId, UpdateExpertDetailsDto updateDetails)
    {
        var existingExpert = await _accountRepository.GetAccountById(expertId);
        if (existingExpert == null)
            throw new ServiceException($"Expert profile with expertId:{expertId} is not found",
                new NotFoundException());
        try
        {
            existingExpert.Username = updateDetails.Username;
            existingExpert.PhoneNumber = updateDetails.Phone;
            existingExpert.Address = updateDetails.Address;
            existingExpert.AvatarUrl = updateDetails.Avatar;
            existingExpert.Status = updateDetails.Status;
            await _accountRepository.Update(existingExpert);

            var  expertProfile = await _expertRepository.GetExpertDetailsByExpertId(expertId);
            expertProfile.Name = updateDetails.Name;
            expertProfile.Description = updateDetails.Description;
            expertProfile.Major = updateDetails.Major;
            await _expertRepository.Update(expertProfile);

            return existingExpert.Id;
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }
}