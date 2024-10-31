using Domain.DTOs.ReviewMilestone;
using Domain.Entities;

namespace Application.Interfaces.IServices;

public interface IReviewMilestoneService
{
    Task<IEnumerable<ReviewMilestoneDto>> GetAll();

    Task<ReviewMilestoneDto> GetById(int id);

    Task<ReviewMilestoneDto> CreateReviewMilestone(AddReviewMilestoneDto dto);

    Task<ReviewMilestoneDto> UpdateReviewMilestone(int id, UpdateReviewMilestoneDto dto);
}
