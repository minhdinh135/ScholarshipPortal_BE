using Application.Exceptions;
using Application.Interfaces.IServices;
using Domain.DTOs.Common;
using Domain.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/requests")]
public class RequestController : ControllerBase
{
    private readonly IRequestService _requestService;

    public RequestController(IRequestService requestService)
    {
        _requestService = requestService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRequests()
    {
        var requests = await _requestService.GetAllRequests();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all requests successfully", requests));
    }

    [HttpGet("check-applicant-requests")]
    public async Task<IActionResult> CheckUserRequest(int serviceId, int applicantId)
    {
        bool hasRequested = await _requestService.HasUserRequestedService(serviceId, applicantId);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Check user request successfully", hasRequested));
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetRequestById(int id)
    {
        try
        {
            var request = await _requestService.GetRequestById(id);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Get request successfully", request));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpGet("with-applicant-and-request-details/{id}")]
    public async Task<IActionResult> GetWithApplicantAndRequestDetails(int id)
    {
        try
        {
            var request = await _requestService.GetWithApplicantAndRequestDetails(id);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Get request successfully", request));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

	[HttpGet("get-by-service/{serviceId}")]
	public async Task<IActionResult> GetByService(int serviceId)
	{
		try
		{
			var requests = await _requestService.GetByServiceId(serviceId);
			if (requests == null)
				return NotFound("No requests found for this service.");

			return Ok(new ApiResponse(StatusCodes.Status200OK, "Retrieved requests successfully", requests));
		}
		catch (Exception ex)
		{
			return StatusCode(500, "Error retrieving data from the database." + ex.Message);
		}
	}

	[HttpPost]
    public async Task<IActionResult> CreateRequest(AddRequestDto addRequestDto)
    {
        try
        {
            var createdRequest = await _requestService.CreateRequest(addRequestDto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Create request successfully", createdRequest));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRequest(int id, UpdateRequestDto updateRequestDto)
    {
        try
        {
            var updatedRequest = await _requestService.UpdateRequest(id, updateRequestDto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update request successfully", updatedRequest));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
}
