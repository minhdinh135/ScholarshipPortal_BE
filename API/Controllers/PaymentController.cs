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

    [HttpGet("products")]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _stripeService.GetAllProducts();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get products successfully", products));
    }
}