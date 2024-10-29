using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CountryRepository : GenericRepository<Country>, ICountryRepository
{
    public CountryRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }
}