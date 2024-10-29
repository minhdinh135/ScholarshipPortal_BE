using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Country;

namespace Application.Services;

public class CountryService : ICountryService
{
    private readonly IMapper _mapper;
    private readonly ICountryRepository _countryRepository;

    public CountryService(IMapper mapper, ICountryRepository countryRepository)
    {
        _mapper = mapper;
        _countryRepository = countryRepository;
    }

    public async Task<IEnumerable<CountryDto>> GetAllCountries()
    {
        var countries = await _countryRepository.GetAll();

        return _mapper.Map<IEnumerable<CountryDto>>(countries);
    }

    public async Task<CountryDto> GetCountryById(int id)
    {
        var country = await _countryRepository.GetById(id);

        return _mapper.Map<CountryDto>(country);
    }
}