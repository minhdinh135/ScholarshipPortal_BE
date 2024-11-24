using Domain.DTOs.Expert;
using Domain.DTOs.Funder;

namespace Application.Interfaces.IServices;

public interface IFunderService
{
    Task<FunderProfileDto> GetFunderDetailsByFunderId(int funderId);
    Task<FunderProfileDto> AddFunderDetails(AddFunderDetailsDto addFunderDetailsDto);
    Task<FunderProfileDto> UpdateFunderDetails(int funderId, UpdateFunderDetailsDto updateFunderDetailsDto);
    Task<IEnumerable<ExpertDetailsDto>> GetExpertsByFunderId(int id);
}