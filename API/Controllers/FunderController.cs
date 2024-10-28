using Application.Exceptions;
using Application.Interfaces.IServices;
using Domain.DTOs.Common;
using Domain.DTOs.Funder;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/funders")]
public class FunderController : ControllerBase
{
    private readonly IFunderService _funderService;

    public FunderController(IFunderService funderService)
    {
        _funderService = funderService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFunderDetails(int id)
    {
        var funder = await _funderService.GetFunderDetailsByFunderId(id);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get Funder details successfully", funder));
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> AddFunderDetails(AddFunderDetailsDto addFunderDetailsDto)
    {
        try
        {
            var addedFunderDetails = await _funderService.AddFunderDetails(addFunderDetailsDto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Add funder details successully", addedFunderDetails));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFunderDetails(int id, UpdateFunderDetailsDto updateFunderDetailsDto)
    {
        try
        {
            var updatedFunder = await _funderService.UpdateFunderDetails(id, updateFunderDetailsDto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update funder details successfully", updatedFunder));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
}