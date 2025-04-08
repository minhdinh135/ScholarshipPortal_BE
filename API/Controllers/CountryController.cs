using Application.Interfaces.IServices;
using Domain.Constants;
using Domain.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route(UriConstant.COUNTRY_BASE_URI)]
public class CountryController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCountries()
    {
        var countries = await _countryService.GetAllCountries();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all countries successfully", countries));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCountryById(int id)
    {
        var country = await _countryService.GetCountryById(id);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get country successfully", country));
    }
}