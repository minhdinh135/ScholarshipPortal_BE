using Domain.DTOs.Feedback;

namespace Application.Interfaces.IServices;

public interface IFeedbackService
{
    Task<IEnumerable<FeedbackDto>> GetAllFeedbacks();
    Task<FeedbackDto> GetFeedbackById(int id);
    Task<FeedbackDto> AddFeedback(AddFeedbackDto addFeedbackDto);
    Task<FeedbackDto> UpdateFeedback(int id, UpdateFeedbackDto updateFeedbackDto);
	Task<bool> FeedbackExists(int applicantId, int serviceId);
}