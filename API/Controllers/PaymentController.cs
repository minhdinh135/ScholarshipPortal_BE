using Application.Exceptions;
using Application.Interfaces.IServices;
using Domain.DTOs.Common;
using Domain.DTOs.Payment;
using Infrastructure.ExternalServices.Stripe;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using TransferRequest = Domain.DTOs.Payment.TransferRequest;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/payments")]
public class PaymentController : ControllerBase
{
    private readonly StripeSettings _stripeSettings;
    private readonly IPaymentService _paymentService;
    private readonly IAccountService _accountService;
    private readonly IEmailService _emailService;

    public PaymentController(IOptions<StripeSettings> stripeSettings,
        IPaymentService paymentService, IAccountService accountService, IEmailService emailService)
    {
        _stripeSettings = stripeSettings.Value;
        _paymentService = paymentService;
        _accountService = accountService;
        _emailService = emailService;
    }

    [HttpPost("stripe-checkout")]
    public async Task<IActionResult> CreateCheckoutSession(CheckoutSessionRequest checkoutSessionRequest)
    {
        try
        {
            var result = await _paymentService.CreateCheckoutSession(checkoutSessionRequest);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Create checkout session successfully", result));
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

            if (stripeEvent.Type == EventTypes.CheckoutSessionCompleted)
            {
                var session = stripeEvent.Data.Object as Session;

                var amount = (decimal)(session.AmountTotal / 100);
                var senderId = int.Parse(session.Metadata["senderId"]);
                var receiverId = int.Parse(session.Metadata["receiverId"]);
                var description = session.Metadata["description"];
                var paymentMethod = session.Metadata["paymentMethod"];

                var receiverWallet = await _accountService.GetWalletByUserId(receiverId);
                await _accountService.UpdateWalletBalance(receiverId, receiverWallet.Balance + amount);

                AddTransactionDto addTransactionDto = new AddTransactionDto
                {
                    Amount = amount,
                    SenderId = senderId,
                    PaymentMethod = paymentMethod,
                    Description = description,
                    ReceiverId = receiverId,
                    TransactionId = session.PaymentIntentId
                };
                await _paymentService.AddTransaction(addTransactionDto);

                await _emailService.SendPaymentReceipt(session.CustomerEmail, amount, session.PaymentIntentId);
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