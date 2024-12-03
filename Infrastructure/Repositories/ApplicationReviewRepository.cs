using Application.Interfaces.IRepositories;
using Domain.Constants;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ApplicationReviewRepository : GenericRepository<ApplicationReview>, IApplicationReviewRepository
{
    public ApplicationReviewRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }
}