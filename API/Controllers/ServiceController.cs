using Application.Exceptions;
using Application.Interfaces.IServices;
using Domain.DTOs.Common;
using Domain.DTOs.Service;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/services")]
public class ServiceController : ControllerBase
{
    private readonly IServiceService _serviceService;

    public ServiceController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllServices()
    {
        var services = await _serviceService.GetAllServices();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all services successfully", services));
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetServiceById(int id)
    {
        try
        {
            var service = await _serviceService.GetServiceById(id);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Get service successfully", service));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

	[HttpGet("paginated")]
	public async Task<IActionResult> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10,
	[FromQuery] string sortBy = default, [FromQuery] string sortOrder = default)
	{
		var services = await _serviceService.GetAll(pageIndex, pageSize, sortBy, sortOrder);

		return Ok(new ApiResponse(StatusCodes.Status200OK, "Get services successfully", services));
	}


	[HttpGet("by-provider-id/{providerId}")]
	public async Task<IActionResult> GetServicesByProviderId([FromRoute] int providerId)
	{
		var services = await _serviceService.GetServicesByProviderId(providerId);
		return Ok(new ApiResponse(StatusCodes.Status200OK, "Get services by provider ID successfully", services));
	}


	[HttpPost]
    public async Task<IActionResult> AddService(AddServiceDto addServiceDto)
    {
        try
        {
            var addedService = await _serviceService.AddService(addServiceDto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Add service successfully", addedService));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateService(int id, UpdateServiceDto updateServiceDto)
    {
        try
        {
            var updatedService = await _serviceService.UpdateService(id, updateServiceDto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update service successfully", updatedService));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
}