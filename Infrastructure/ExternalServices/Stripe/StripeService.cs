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
            SuccessUrl = _stripeSettings.RedirectBaseUrl + "/payment/result?status=successful",
            CancelUrl = _stripeSettings.RedirectBaseUrl + "/payment/result?status=failed",
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