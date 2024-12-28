using Application.Interfaces.IRepositories;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class CountryRepository : GenericRepository<Country>, ICountryRepository;