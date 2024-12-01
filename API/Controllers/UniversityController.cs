using Application.Exceptions;
using Application.Interfaces.IServices;
using Domain.DTOs.Common;
using Domain.DTOs.University;
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

    [HttpPost]
    public async Task<IActionResult> CreateUniversity(
        AddUniversityDto dto)
    {
        var result =
            await _universityService.CreateUniversity(dto);

        if (result == null)
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Create university failed"));

        return Created("api/universities/" + result, new ApiResponse(
            StatusCodes.Status201Created, $"Create university successfully with id:{result}",
            new { Id = result }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateScholarshipProgram(int id,
        UpdateUniversityDto dto)
    {
        try
        {
            var result = await _universityService.UpdateUniversity(id, dto);
            return Ok(new ApiResponse(StatusCodes.Status200OK,
                $"Update university with id:{result} successfully", result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

}
