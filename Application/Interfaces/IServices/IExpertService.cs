using Domain.DTOs.Expert;

namespace Application.Interfaces.IServices;

public interface IExpertService
{
    Task<ExpertProfileDto> GetExpertProfileByExpertId(int expertId);

    Task<ExpertProfileDto> CreateExpertProfile(CreateExpertDetailsDto createExpertDetailsDto);

    Task<ExpertProfileDto> UpdateExpertProfile(int expertId, UpdateExpertDetailsDto updateExpertDetailsDto);

}