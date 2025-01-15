using xpanse.sdk.Models;
using Xunit;

namespace FunctionalTests;

public class WebhookSubscription
{
    private readonly xpanse.sdk.WebhookSubscription _service;

    public WebhookSubscription()
    {
        _service = new xpanse.sdk.WebhookSubscription();
    }
    
    private WebhookSubscriptionData CreateWebhookSubscription()
    {
        return _service.CreateWebhookSubscription(new NewWebhookSubscription
        {
            Url = "https://webhook.site",
            Types = [WebhookType.Transaction],
        });
    }
    
    [Fact]
    public void CreateSubscription()
    {
        
        var result = CreateWebhookSubscription();

        Assert.NotNull(result);
        Assert.NotNull(result.WebhookSubscriptionId);
    }
    
    [Fact]
    public void GetSubscription()
    {
        var webhook = CreateWebhookSubscription();
        
        var result = _service.GetWebhookSubscription(webhook.WebhookSubscriptionId);
        
        Assert.NotNull(result);
        Assert.Equal(webhook.WebhookSubscriptionId, result.WebhookSubscriptionId);
    }
    
    [Fact]
    public void DeleteSubscription()
    {
        var webhook = CreateWebhookSubscription();
        
        var result = _service.DeleteWebhookSubscription(webhook.WebhookSubscriptionId);
        
        Assert.NotNull(result);
        Assert.Equal(webhook.WebhookSubscriptionId, result.WebhookSubscriptionId);
    }
    
    [Fact]
    public void SearchSubscription()
    {
        var webhook = CreateWebhookSubscription();
        
        var result = _service.SearchWebhookSubscription(new WebhookSubscriptionSearch
        {
            Id = webhook.WebhookSubscriptionId,
        });
        
        Assert.NotNull(result);
        Assert.Equal(1, result.Count);
        Assert.Equal(webhook.WebhookSubscriptionId, result.WebhookSubscriptions[0].WebhookSubscriptionId);
    }
    
}