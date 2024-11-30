using Domain.DTOs.Expert;
using Domain.DTOs.Funder;

namespace Application.Interfaces.IServices;

public interface IFunderService
{
    Task<FunderProfileDetails> GetFunderDetailsByFunderId(int funderId);
    Task<FunderProfileDto> AddFunderDetails(int funderId, AddFunderDetailsDto addFunderDetailsDto);
    Task<int> UpdateFunderDetails(int funderId, UpdateFunderDetailsDto updateFunderDetailsDto);
    Task<IEnumerable<ExpertDetailsDto>> GetExpertsByFunderId(int id);
}