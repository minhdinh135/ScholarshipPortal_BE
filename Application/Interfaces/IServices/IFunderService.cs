using Domain.DTOs.Funder;
using Domain.Entities;

namespace Application.Interfaces.IServices;

public interface IFunderService
{
    Task<FunderProfileDto> GetFunderDetailsByFunderId(int funderId);
    Task<FunderProfileDto> AddFunderDetails(AddFunderDetailsDto addFunderDetailsDto);
    Task<FunderProfileDto> UpdateFunderDetails(int funderId, UpdateFunderDetailsDto updateFunderDetailsDto);
}