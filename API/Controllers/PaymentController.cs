using Application.Interfaces.IServices;
using Domain.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/payments")]
public class PaymentController : ControllerBase
{
    private readonly IStripeService _stripeService;

    public PaymentController(IStripeService stripeService)
    {
        _stripeService = stripeService;
    }

    [HttpPost("/pay")]
    public async Task<IActionResult> Pay(int amount)
    {
        var payment = await _stripeService.Pay(amount);

        return Ok(new ApiResponse(StatusCodes.Status200OK, $"Pay with amount {amount} successfully", payment));
    }
    
    [HttpGet("products")]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _stripeService.GetAllProducts();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get products successfully", products));
    }
}