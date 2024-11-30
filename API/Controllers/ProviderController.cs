using Application.Exceptions;
using Application.Interfaces.IServices;
using Domain.DTOs.Common;
using Domain.DTOs.Funder;
using Domain.DTOs.Provider;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/providers")]
public class ProviderController : ControllerBase
{
    private readonly IProviderService _providerService;

    public ProviderController(IProviderService providerService)
    {
        _providerService = providerService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProviderDetails(int id)
    {
        var provider = await _providerService.GetProviderDetailsByProviderId(id);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get Provider details successfully", provider));
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> AddProviderDetails(int id, AddProviderDetailsDto addProviderDetailsDto)
    {
        try
        {
            var addedProviderDetails = await _providerService.AddProviderDetails(id, addProviderDetailsDto);
    
            return Ok(new ApiResponse(StatusCodes.Status200OK, "Add provider details successully", addedProviderDetails));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProviderDetails(int id, UpdateProviderDetailsDto updateProviderDetailsDto)
    {
        try
        {
            var updatedProvider = await _providerService.UpdateProviderDetails(id, updateProviderDetailsDto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update provider details successfully", updatedProvider));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
}