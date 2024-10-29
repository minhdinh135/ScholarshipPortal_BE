using Application.Interfaces.IServices;
using Domain.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/skills")]
public class SkillController : ControllerBase
{
    private readonly ISkillService _skillService;

    public SkillController(ISkillService skillService)
    {
        _skillService = skillService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSkills()
    {
        var skills = await _skillService.GetAllSkills();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all skills successfully", skills));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSkillById(int id)
    {
        var skill = await _skillService.GetSkillById(id);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get skill successfully", skill));
    }
}