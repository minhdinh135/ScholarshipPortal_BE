using Application.Exceptions;
using Application.Interfaces.IServices;
using Domain.Constants;
using Domain.DTOs.Common;
using Domain.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route(UriConstant.REQUEST_BASE_URI)]
public class RequestController : ControllerBase
{
    private readonly IRequestService _requestService;

    public RequestController(IRequestService requestService)
    {
        _requestService = requestService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRequests([FromQuery] int applicantId)
    {
        var requests = await _requestService.GetAllRequests(applicantId);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all requests successfully", requests));
    }

    [HttpGet("check-applicant-requests")]
    public async Task<IActionResult> CheckUserRequest(int serviceId, int applicantId)
    {
        bool hasRequested = await _requestService.HasUserRequestedService(serviceId, applicantId);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Check user request successfully", hasRequested));
    }

	[HttpDelete("cancel-request/{id}")]
	public async Task<IActionResult> CancelRequest(int id)
	{
		var result = await _requestService.CancelRequestAsync(id);
		if (result)
		{
			return Ok(new ApiResponse(StatusCodes.Status200OK, "Request cancelled successfully"));
		}
		return NotFound(new ApiResponse(StatusCodes.Status404NotFound, "Request not found"));
	}

	[HttpPut("finish-request/{id}")]
	public async Task<IActionResult> UpdateStatusFinish(int id)
	{
		try
		{
			var updatedRequest = await _requestService.UpdateRequestStatusFinish(id);
			return Ok(new ApiResponse(StatusCodes.Status200OK, "Request status updated to 'Finished' successfully", updatedRequest));
		}
		catch (Exception ex)
		{
			return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Error updating request status", ex.Message));
		}
	}

	//[HttpGet("applicant/{applicantId}")]
	//public async Task<IActionResult> GetRequestsByApplicantId(int applicantId)
	//{
	//	try
	//	{
	//		var requests = await _requestService.GetRequestByApplicantId(applicantId);
	//		return Ok(new ApiResponse(StatusCodes.Status200OK, "Requests retrieved successfully", requests));
	//	}
	//	catch (ServiceException e)
	//	{
	//		return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
	//	}
	//}


	[HttpGet("{id}")]
    public async Task<IActionResult> GetRequestById(int id)
    {
        try
        {
            var request = await _requestService.GetRequestById(id);
            if(request == null) { return BadRequest(); }
            if(request.Status == "Finished" && request.UpdatedAt.AddDays(3) < DateTime.Now )
            {

            }

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
    public async Task<IActionResult> CreateRequest(ApplicantCreateRequestDto applicantCreateRequestDto)
    {
        try
        {
            var createdRequest = await _requestService.CreateRequest(applicantCreateRequestDto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Create request successfully", createdRequest));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRequestResult(int id, ProviderUpdateRequestDto providerUpdateRequestDto)
    {
        try
        {
            var updatedRequest = await _requestService.UpdateRequestResult(id, providerUpdateRequestDto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update request successfully", updatedRequest));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
}
