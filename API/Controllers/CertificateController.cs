using Application.Interfaces.IServices;
using Domain.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/certificates")]
public class CertificateController : ControllerBase
{
    private readonly ICertificateService _certificateService;

    public CertificateController(ICertificateService certificateService)
    {
        _certificateService = certificateService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCertificates()
    {
        var certificates = await _certificateService.GetAllCertificates();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all certificates successfully", certificates));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCertificateById(int id)
    {
        var certificate = await _certificateService.GetCertificateById(id);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get certificate successfully", certificate));
    }
}