using Application.Interfaces.IServices;
using Domain.Constants;
using Domain.DTOs.Payment;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace Infrastructure.ExternalServices.Stripe;

public class StripeService : IStripeService
{
    private readonly StripeSettings _stripeSettings;

    public StripeService(IOptions<StripeSettings> stripeSettings)
    {
        _stripeSettings = stripeSettings.Value;
        StripeConfiguration.ApiKey = _stripeSettings.ApiKey;
    }

    public async Task<string> CreatePayment(string productId)
    {
        var priceOptions = new PriceCreateOptions
        {
            Currency = "usd",
            UnitAmount = 1000,
            Product = productId,
        };
        var priceService = new PriceService();
        var price = await priceService.CreateAsync(priceOptions);


        var paymentLinkOptions = new PaymentLinkCreateOptions
        {
            LineItems = new List<PaymentLinkLineItemOptions>
            {
                new PaymentLinkLineItemOptions { Price = price.Id, Quantity = 1 },
            },
            AfterCompletion = new PaymentLinkAfterCompletionOptions()
            {
                Type = "redirect",
                Redirect = new PaymentLinkAfterCompletionRedirectOptions
                {
                    Url = "http://localhost:5173/payment/result",
                },
            }
        };
        var paymentLinkService = new PaymentLinkService();
        var paymentLink = await paymentLinkService.CreateAsync(paymentLinkOptions);

        return paymentLink.Url;
    }

    public async Task<CheckoutSessionResponse> CreateCheckoutSession(string email, CheckoutSessionRequest checkoutSessionRequest)
    {
        var sessionCreateOptions = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string>
            {
                "card",
            },
            CustomerEmail = email,
            Mode = "payment",
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Amount"
                        },
                        UnitAmount = (int)(checkoutSessionRequest.Amount * 100)
                    },
                    Quantity = 1,
                },
            },
            Metadata = new Dictionary<string, string>
            {
                { "senderId", checkoutSessionRequest.SenderId.ToString() },
                { "receiverId", checkoutSessionRequest.ReceiverId.ToString() },
                { "description", checkoutSessionRequest.Description },
                { "paymentMethod", PaymentMethodEnum.Stripe.ToString() }
            },
            SuccessUrl = "http://localhost:5173/payment/result?status=successful",
            CancelUrl = "http://localhost:5173/payment/result?status=failed",
        };

        var sessionService = new SessionService();
        var session = await sessionService.CreateAsync(sessionCreateOptions);

        return new CheckoutSessionResponse
        {
            SessionUrl = session.Url,
            PublishableKey = _stripeSettings.PublishableKey
        };
    }
}