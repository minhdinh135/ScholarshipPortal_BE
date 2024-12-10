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
    private readonly INotificationService _notificationService;
    private IAccountService _accountService;

    public ServiceController(IServiceService serviceService, INotificationService notificationService,
        IAccountService accountService)
    {
        _serviceService = serviceService;
        _notificationService = notificationService;
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllServices([FromQuery] ListOptions listOptions)
    {
        var services = await _serviceService.GetAllServices(listOptions);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all services successfully", services));
    }

    [HttpGet("count")]
    public async Task<IActionResult> CountServices()
    {
        var services = await _serviceService.GetAllServices();
        var count = services.Count();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all services successfully", count));
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

	[HttpGet("by-provider-paginated/{id}")]
	public async Task<IActionResult> GetAllByProviderId(int id, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10,
	[FromQuery] string sortBy = default, [FromQuery] string sortOrder = default)
	{
		var services = await _serviceService.GetAllByProviderId(id, pageIndex, pageSize, sortBy, sortOrder);
        await _serviceService.CheckSubscriptionEndDateProvider(id);

		return Ok(new ApiResponse(StatusCodes.Status200OK, "Get services successfully", services));
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
            var accounts = await _accountService.GetAll();
            
            foreach(var account in accounts)
            {
                await _notificationService.SendDataMessage(account.Id.ToString(), new Dictionary<string, string>
                {
					{ "entity", "service" }
				});
            }

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
