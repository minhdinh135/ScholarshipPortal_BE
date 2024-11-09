using Application.Exceptions;
using Application.Interfaces.IServices;
using Domain.DTOs.Common;
using Domain.DTOs.Payment;
using Infrastructure.ExternalServices.Stripe;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/payments")]
public class PaymentController : ControllerBase
{
    private readonly StripeSettings _stripeSettings;
    private readonly IStripeService _stripeService;
    private readonly IPaymentService _paymentService;

    public PaymentController(IOptions<StripeSettings> stripeSettings, IStripeService stripeService,
        IPaymentService paymentService)
    {
        _stripeSettings = stripeSettings.Value;
        _stripeService = stripeService;
        _paymentService = paymentService;
    }

    // [HttpPost("/pay")]
    // public async Task<IActionResult> Pay(int amount)
    // {
    //     var payment = await _stripeService.Pay(amount);
    //
    //     return Ok(new ApiResponse(StatusCodes.Status200OK, $"Pay with amount {amount} successfully", payment));
    // }

    // [HttpGet("products")]
    // public async Task<IActionResult> GetAllProducts()
    // {
    //     var products = await _stripeService.GetAllProducts();
    //
    //     return Ok(new ApiResponse(StatusCodes.Status200OK, "Get products successfully", products));
    // }

    // [HttpPost("create-payment")]
    // public async Task<IActionResult> CreatePayment()
    // {
    //     var paymentUrl = await _stripeService.CreatePayment("prod_R7QW3darhZATeM");
    //
    //     return Ok(paymentUrl);
    // }

    // [HttpPost("create-invoice")]
    // public async Task<IActionResult> CreateInvoice(InvoiceRequest invoiceRequest)
    // {
    //     try
    //     {
    //         var invoiceUrl = await _paymentService.CreateInvoice(invoiceRequest);
    //
    //         return Ok(new ApiResponse(StatusCodes.Status200OK, "Create invoice successfully", invoiceUrl));
    //     }
    //     catch (ServiceException e)
    //     {
    //         return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
    //     }
    // }

    [HttpPost("transfer-money")]
    public async Task<IActionResult> TransferMoney(TransferRequest transferRequest)
    {
        try
        {
            await _paymentService.TransferMoney(transferRequest);

            return Ok(new ApiResponse(StatusCodes.Status200OK,
                $"Transfer from senderId:{transferRequest.SenderId} to {transferRequest.ReceiverId} successfully"));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    // [HttpPost("webhook")]
    // public async Task<IActionResult> HandleWebhook()
    // {
    //     var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
    //
    //     try
    //     {
    //         var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"],
    //             _stripeSettings.WebhookSecret);
    //
    //         // Handle the event
    //         // If on SDK version < 46, use class Events instead of EventTypes
    //         if (stripeEvent.Type == EventTypes.InvoicePaymentSucceeded)
    //         {
    //             var invoice = stripeEvent.Data.Object as Invoice;
    //         }
    //         // ... handle other event types
    //         else
    //         {
    //             // Unexpected event type
    //             Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
    //         }
    //
    //         return Ok();
    //     }
    //     catch (StripeException e)
    //     {
    //         return BadRequest();
    //     }
    // }
}