using Application.Interfaces.IServices;
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

    public async Task<string> CreatePayment(string priceId)
    {
        var domain = "http://localhost:4242";
        var options = new SessionCreateOptions
        {
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    Price = priceId,
                    Quantity = 1
                },
            },
            Mode = "payment",
            SuccessUrl = domain + "?success=true",
            CancelUrl = domain + "?canceled=true",
        };

        var service = new SessionService();
        Session session = service.Create(options);

        return session.Url;
    }

    public async Task<object> Pay(int amount)
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = amount,
            Currency = "usd",
            AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
            {
                Enabled = true,
            },
        };
        var service = new PaymentIntentService();
        var paymentIntent = service.Create(options);

        return paymentIntent;
    }

    public async Task<List<object>> GetAllProducts()
    {
        var options = new ProductListOptions { Limit = 3 };
        var service = new ProductService();
        StripeList<Product> products = service.List(options);

        return [products.ToList()];
    }
}