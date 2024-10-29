using Domain.DTOs.Country;

namespace Application.Interfaces.IServices;

public interface ICountryService
{
    Task<IEnumerable<CountryDto>> GetAllCountries();
    Task<CountryDto> GetCountryById(int id);
}