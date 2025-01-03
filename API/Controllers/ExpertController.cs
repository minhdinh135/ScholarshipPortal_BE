using Application.Exceptions;
using Application.Interfaces.IServices;
using Domain.Constants;
using Domain.DTOs.Authentication;
using Domain.DTOs.Common;
using Domain.DTOs.Expert;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route(UriConstant.EXPERT_BASE_URI)]
public class ExpertController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IApplicationService _applicationService;
    private readonly IExpertService _expertService;

    public ExpertController(IAccountService accountService,
        IApplicationService applicationService, IExpertService expertService)
    {
        _accountService = accountService;
        _applicationService = applicationService;
        _expertService = expertService;
    }

    [HttpGet("funder/{funderId}")]
    public async Task<IActionResult> GetAllExpertsByFunder(int funderId)
    {
        var experts = await _expertService.GetAllExpertProfilesByFunder(funderId);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get experts by funder successfully", experts));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllExperts()
    {
        var experts = await _expertService.GetAllExpertProfileByExpert();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get experts successfully", experts));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExpertDetails(int id)
    {
        var expert = await _expertService.GetExpertProfileByExpertId(id);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get Expert details successfully", expert));
    }

    [HttpGet("{id}/assigned-applications")]
    public async Task<IActionResult> GetExpertAssignedApplications(int id)
    {
        var assignedApplications = await _applicationService.GetExpertAssignedApplications(id);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get Expert assigned applications successfully",
            assignedApplications));
    }

    [HttpPost]
    public async Task<IActionResult> AddExpertDetails(CreateExpertDetailsDto dto)
    {
        try
        {
            var addedProfile = await _accountService.AddAccount(new RegisterDto
            {
                Username = dto.Name,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Password = dto.Password,
                Address = dto.Address,
                AvatarUrl = dto.AvatarUrl,
                LoginWithGoogle = dto.LoginWithGoogle,
                Status = dto.Status,
                FunderId = dto.FunderId,
                RoleId = dto.RoleId
            });
            dto.ExpertId = addedProfile.Id;
            var createdExpertDetails = await _expertService.CreateExpertProfile(dto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Create expert details successully",
                createdExpertDetails));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpertDetails(int id, UpdateExpertDetailsDto updateExpertDetailsDto)
    {
        try
        {
            var updatedExpertDetails = await _expertService.UpdateExpertProfile(id, updateExpertDetailsDto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update expert details successfully",
                updatedExpertDetails));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
}