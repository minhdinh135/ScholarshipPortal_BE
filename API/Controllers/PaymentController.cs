using Application.Exceptions;
using Application.Interfaces.IServices;
using Domain.Constants;
using Domain.DTOs.Common;
using Domain.DTOs.Payment;
using Infrastructure.ExternalServices.Stripe;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

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

    [HttpPost("checkout-session")]
    public async Task<IActionResult> CreateCheckoutSession(TransferRequest transferRequest)
    {
        try
        {
            var result = await _paymentService.CreateCheckoutSession(transferRequest);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Create session successfully", result));
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
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound,
                    "No transactions found for this wallet"));
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
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                _stripeSettings.WebhookSecret
            );

            // if (stripeEvent.Type == EventTypes.InvoicePaid)
            // {
            //     var invoice = stripeEvent.Data.Object as Invoice;
            //     var customer = await _stripeService.GetCustomer(invoice.CustomerId) as Customer;
            //     await _accountService.UpdateWalletBalance(int.Parse(invoice.Metadata["accountId"]),
            //         -customer.Balance / 100);
            //
            //     await _emailService.SendInvoiceReceipt(customer.Email, invoice.Total, invoice.Id);
            // }
            if (stripeEvent.Type == EventTypes.CheckoutSessionCompleted)
            {
                var session = stripeEvent.Data.Object as Session;
                // await _paymentService.AddTransaction((decimal)(session.AmountTotal / 100),
                //     PaymentMethodEnum.Card.ToString(),
                //     "Payment succeeded", session.Id, int.Parse(session.Metadata["senderId"]),
                //     int.Parse(session.Metadata["receiverId"]));
                await _paymentService.TransferMoney(new TransferRequest
                {
                    Amount = (decimal)(session.AmountTotal / 100), SenderId = int.Parse(session.Metadata["senderId"]),
                    ReceiverId = int.Parse(session.Metadata["receiverId"])
                });

                await _emailService.SendPaymentReceipt(session.CustomerEmail, (decimal)(session.AmountTotal / 100),
                    session.Id);
            }
            else if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            }
            else
            {
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }

            return Ok();
        }
        catch (StripeException e)
        {
            return BadRequest(e.Message);
        }
        catch (ServiceException e)
        {
            return BadRequest(e.Message);
        }
    }
}