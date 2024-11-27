using Application.Interfaces.IServices;
using Domain.DTOs.Common;
using Domain.DTOs.Subscription;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/subscriptions")]
public class SubscriptionController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSubscriptions()
    {
        var allSubscriptions = await _subscriptionService.GetAllSubscriptions();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all subscriptions successfully", allSubscriptions));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubscriptionById(int id)
    {
        try
        {
            var allSubscriptions = await _subscriptionService.GetSubscriptionById(id);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Get subscription successfully", allSubscriptions));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddSubscription(AddSubscriptionDto addSubscriptionDto)
    {
        try
        {
            var result = await _subscriptionService.AddSubscription(addSubscriptionDto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, $"Add subscription with ID: {result} successfully",
                result));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubscription(int id, UpdateSubscriptionDto updateSubscriptionDto)
    {
        try
        {
            await _subscriptionService.UpdateSubscription(id, updateSubscriptionDto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update subscription successfully"));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

	[HttpGet("by-provider/{providerId}")]
	public async Task<IActionResult> GetSubscriptionByProviderId(int providerId)
	{
		try
		{
			var subscription = await _subscriptionService.GetSubscriptionByProviderId(providerId);

			return Ok(new ApiResponse(StatusCodes.Status200OK, "Get subscription by provider ID successfully", subscription));
		}
		catch (Exception e)
		{
			return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
		}
	}


}