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
    private readonly IAccountRepository _accountRepository;

    public FunderService(IMapper mapper, IFunderRepository funderRepository, IAccountRepository accountRepository)
    {
        _mapper = mapper;
        _funderRepository = funderRepository;
        _accountRepository = accountRepository;
    }

    public async Task<FunderProfileDetails> GetFunderDetailsByFunderId(int funderId)
    {
        var funder = await _funderRepository.GetFunderDetailsByFunderId(funderId);

        return _mapper.Map<FunderProfileDetails>(funder);
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

    public async Task<int> UpdateFunderDetails(int funderId, UpdateFunderDetailsDto updateDetails)
    {
        var existingFunder = await _accountRepository.GetAccountById(funderId);
        if (existingFunder == null)
            throw new ServiceException($"Funder with funderId:{funderId} is not found", new NotFoundException());

        try
        {
            existingFunder.Username = updateDetails.Username;
            existingFunder.PhoneNumber = updateDetails.Phone;
            existingFunder.Address = updateDetails.Address;
            existingFunder.AvatarUrl = updateDetails.Avatar;
            existingFunder.Status = updateDetails.Status;
            await _accountRepository.Update(existingFunder);

            var funderProfile = await _funderRepository.GetFunderDetailsByFunderId(funderId);
            funderProfile.OrganizationName = updateDetails.OrganizationName;
            funderProfile.ContactPersonName = updateDetails.ContactPersonName;
            
            List<FunderDocument> funderDocuments = updateDetails.FunderDocuments.Select(d => new FunderDocument
            {
                Name = d.Name,
                Type = d.Type,
                FileUrl = d.FileUrl
            }).ToList();
            funderDocuments.ForEach(d => d.FunderProfileId = funderProfile.Id);
            await _funderRepository.UpdateProfileDocuments(funderProfile.Id, funderDocuments);

            return existingFunder.Id;
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<IEnumerable<ExpertDetailsDto>> GetExpertsByFunderId(int id)
    {
        var experts = await _funderRepository.GetExpertsByFunderId(id);

        return _mapper.Map<IEnumerable<ExpertDetailsDto>>(experts);
    }
}