using Application.Exceptions;
using Application.Interfaces.IServices;
using Domain.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/universities")]
public class UniversityController : ControllerBase
{
    private readonly IUniversityService _universityService;

    public UniversityController(IUniversityService universityService)
    {
        _universityService = universityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUniversities()
    {
        var universities = await _universityService.GetAllUniversities();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all universities successfully", universities));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUniversityById(int id)
    {
        try
        {
            var university = await _universityService.GetUniversityById(id);

            return Ok(new ApiResponse(StatusCodes.Status200OK, $"Get university with id:{id} successfully",
                university));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
}