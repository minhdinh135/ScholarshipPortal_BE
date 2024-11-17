using Application.Interfaces.IServices;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using Account = Domain.Entities.Account;

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

    public async Task<string> CreateCheckoutSession(string email, decimal amount)
    {
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string>
            {
                "card",
                "us_bank_account"
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
                        UnitAmount = (int)(amount * 100)
                    },
                    Quantity = 1,
                },
            },
            Metadata = new Dictionary<string, string>
            {
                { "OrderId", "123456" }, // Include any custom data here
                { "CustomerEmail", "customer@example.com" }
            },
            SuccessUrl = "http://localhost:5173/payment/result?status=successful",
            CancelUrl = "https://localhost:5173/payment/result?status=failed",
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return session.Url;
    }

    public async Task<string> CreateInvoice(string stripeCustomerId, decimal amount,
        Dictionary<string, string> metaData)
    {
        var invoiceOptions = new InvoiceCreateOptions
        {
            Customer = stripeCustomerId,
            CollectionMethod = "send_invoice",
            DaysUntilDue = 30,
            Metadata = metaData
        };
        var invoiceService = new InvoiceService();
        var invoice = await invoiceService.CreateAsync(invoiceOptions);

        // Create an Invoice Item with the Price, and Customer you want to charge
        var invoiceItemOptions = new InvoiceItemCreateOptions
        {
            Customer = stripeCustomerId,
            Amount = (int)(amount * 100),
            Invoice = invoice.Id,
            Description = "Test Invoice Item",
        };
        var invoiceItemService = new InvoiceItemService();
        await invoiceItemService.CreateAsync(invoiceItemOptions);

        await invoiceService.FinalizeInvoiceAsync(invoice.Id);

        // Send the Invoice
        await invoiceService.SendInvoiceAsync(invoice.Id);

        var sentInvoice = await invoiceService.GetAsync(invoice.Id);

        return sentInvoice.HostedInvoiceUrl;
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
        var paymentIntent = await service.CreateAsync(options);

        return paymentIntent;
    }

    public async Task<List<object>> GetAllProducts()
    {
        var options = new ProductListOptions { Limit = 3 };
        var service = new ProductService();
        StripeList<Product> products = await service.ListAsync(options);

        return [products.ToList()];
    }

    public async Task<string> CreateStripeCustomer(Account account, decimal balance)
    {
        var customerOptions = new CustomerCreateOptions
        {
            Name = account.Username,
            Email = account.Email,
            Phone = account.PhoneNumber,
            Balance = -(int)(balance * 100),
        };
        var customerService = new CustomerService();
        var customer = await customerService.CreateAsync(customerOptions);

        var paymentSourceOptions = new CustomerPaymentSourceCreateOptions { Source = "tok_visa" };
        var paymentSourceService = new CustomerPaymentSourceService();
        var paymentSource = await paymentSourceService.CreateAsync(customer.Id, paymentSourceOptions);

        var customerUpdateOptions = new CustomerUpdateOptions
        {
            Source = paymentSource.Id
        };
        await customerService.UpdateAsync(customer.Id, customerUpdateOptions);

        return customer.Id;
    }

    public async Task<object> GetCustomer(string stripeCustomerId)
    {
        var service = new CustomerService();
        var customer = await service.GetAsync(stripeCustomerId);

        return customer;
    }

    public async Task<string> UpdateCustomerBalance(string stripeCustomerId, decimal balance)
    {
        var options = new CustomerUpdateOptions
        {
            Balance = -(int)(balance * 100)
        };
        var service = new CustomerService();
        var updatedCustomer = await service.UpdateAsync(stripeCustomerId, options);

        return updatedCustomer.Id;
    }
}