using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IApplicationReviewRepository : IGenericRepository<ApplicationReview>
{
    Task<IEnumerable<ApplicationReview>> GetApplicationReviewsResult(bool isFirstReview);
}