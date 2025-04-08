using Domain.DTOs.Expert;

namespace Application.Interfaces.IServices;

public interface IExpertService
{
    Task<ExpertDetailsDto> GetExpertProfileByExpertId(int expertId);

    Task<ExpertProfileDto> CreateExpertProfile(CreateExpertDetailsDto createExpertDetailsDto);

    Task<int> UpdateExpertProfile(int expertId, UpdateExpertDetailsDto updateExpertDetailsDto);
    Task<List<ExpertDetailsDto>> GetAllExpertProfileByExpert();
	Task<List<ExpertDetailsDto>> GetAllExpertProfilesByFunder(int funderId);
}