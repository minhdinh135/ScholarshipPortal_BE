﻿namespace Infrastructure.ExternalServices.Stripe;

public class StripeSettings
{
    public string? ApiKey { get; set; }
    public string? PublishableKey { get; set; }
    public string? WebhookSecret { get; set; }
    
    public string? RedirectBaseUrl { get; set; }
}