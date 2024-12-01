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

	[HttpGet]
	public async Task<IActionResult> GetAllFunderDetails()
	{
		var funders = await _funderService.GetAllFunderDetails();
		return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all funder details successfully", funders));
	}

	[HttpGet("{id}")]
    public async Task<IActionResult> GetFunderDetails(int id)
    {
        var funder = await _funderService.GetFunderDetailsByFunderId(id);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get Funder details successfully", funder));
    }
    
    [HttpGet("{id}/experts")]
        public async Task<IActionResult> GetFunderExperts(int id)
        {
            var experts = await _funderService.GetExpertsByFunderId(id);
    
            return Ok(new ApiResponse(StatusCodes.Status200OK, "Get funder experts successfully", experts));
        }

    [HttpPost("{id}")]
    public async Task<IActionResult> AddFunderDetails(int id, AddFunderDetailsDto addFunderDetailsDto)
    {
        try
        {
            var addedFunderDetails = await _funderService.AddFunderDetails(id, addFunderDetailsDto);
    
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