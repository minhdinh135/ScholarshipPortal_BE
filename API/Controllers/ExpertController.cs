using Application.Exceptions;
using Application.Interfaces.IServices;
using Domain.Constants;
using Domain.DTOs.Applicant;
using Domain.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/experts")]
public class ExpertController : ControllerBase
{
    private readonly ILogger<ExpertController> _logger;
    private readonly IAccountService _accountService;
    private readonly IApplicationService _applicationService;

    public ExpertController(ILogger<ExpertController> logger, IAccountService accountService,
            IApplicationService applicationService)
    {
        _logger = logger;
        _accountService = accountService;
        _applicationService = applicationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllExperts()
    {
        var applicants = await _accountService.GetAll();
        applicants = applicants.Where(x => x.RoleName == RoleEnum.EXPERT).ToList();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get experts successfully", applicants));
    }
}

