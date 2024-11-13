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
    private readonly IAccountService _accountService;
    private readonly IEmailService _emailService;

    public PaymentController(IOptions<StripeSettings> stripeSettings, IStripeService stripeService,
        IPaymentService paymentService, IAccountService accountService, IEmailService emailService)
    {
        _stripeSettings = stripeSettings.Value;
        _stripeService = stripeService;
        _paymentService = paymentService;
        _accountService = accountService;
        _emailService = emailService;
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

    [HttpPost("create-invoice")]
    public async Task<IActionResult> CreateInvoice(InvoiceRequest invoiceRequest)
    {
        try
        {
            var invoiceUrl = await _paymentService.CreateInvoice(invoiceRequest);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Create invoice successfully", invoiceUrl));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

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

	[HttpGet("transactions/{walletUserId}")]
	public async Task<IActionResult> GetTransactionsByWalletSenderId(int walletUserId)
	{
		try
		{
			var transactions = await _paymentService.GetTransactionsByWalletUserIdAsync(walletUserId);

			if (transactions == null || transactions.Count() == 0)
			{
				return NotFound(new ApiResponse(StatusCodes.Status404NotFound, "No transactions found for this wallet"));
			}

			return Ok(new ApiResponse(StatusCodes.Status200OK, "Transactions fetched successfully", transactions));
		}
		catch (ServiceException e)
		{
			return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
		}
	}

	[HttpGet("transactions")]
	public async Task<IActionResult> GetAllTransactions()
	{
		try
		{
			var transactions = await _paymentService.GetAllTransactionsAsync();

			if (transactions == null || transactions.Count() == 0)
			{
				return NotFound(new ApiResponse(StatusCodes.Status404NotFound, "No transactions found"));
			}

			return Ok(new ApiResponse(StatusCodes.Status200OK, "Transactions fetched successfully", transactions));
		}
		catch (ServiceException e)
		{
			return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
		}
	}


	[HttpPost("webhook")]
    public async Task<IActionResult> HandleWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        try
        {
            // var stripeEvent = EventUtility.ParseEvent(json);
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                _stripeSettings.WebhookSecret
            );
            
            // Handle the event
            // If on SDK version < 46, use class Events instead of EventTypes
            if (stripeEvent.Type == EventTypes.InvoicePaid)
            {
                var invoice = stripeEvent.Data.Object as Invoice;
                var customer = await _stripeService.GetCustomer(invoice.CustomerId) as Customer;
                await _accountService.UpdateWalletBalance(int.Parse(invoice.Metadata["accountId"]),
                    -customer.Balance / 100);

                await _emailService.SendInvoiceReceipt(customer.Email, invoice.Total, invoice.Id);
            }
            // ... handle other event types
            else
            {
                // Unexpected event type
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }

            return Ok();
        }
        catch (StripeException e)
        {
            return BadRequest(e.Message);
        }
    }
}