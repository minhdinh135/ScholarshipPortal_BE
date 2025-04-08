using Application.Interfaces.IRepositories;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class ApplicationReviewRepository : GenericRepository<Review>, IApplicationReviewRepository;