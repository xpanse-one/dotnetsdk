using System.Collections.Generic;
using System.Linq;
using xpanse.sdk.Models;
using xpanse.sdk.Models.Subscriptions;
using Xunit;

namespace FunctionalTests;

public class Subscription : BaseTest
{
    private static readonly CardRequestInformation CardRequestInformation = new()
    {
        CardNumber = "4111111111111111",
        ExpiryDate = "12/35",
        Ccv = "123"
    };

    private NewPaymentMethodCard GetNewPaymentMethod()
    {
        return new NewPaymentMethodCard
        {
            ProviderId = GetProviderId(),
            PaymentInformation = CardRequestInformation
        };
    }
    
    private SubscriptionCreate GetNewSubscription(string paymentMethodId)
    {
        return new SubscriptionCreate
        {
            EndAfter = new SubscriptionEnd()
            {
                Count = 2
            },
            Retry = new SubscriptionRetryPolicy()
            {
                Maximum = 3,
                Frequency = 1,
                Interval = SubscriptionRetryInterval.Day
            },
            Webhook = new WebhookConfig()
            {
                Url = "https://example.com/webhoo",
                Authorization = "secret"
            },
            PaymentMethodId = paymentMethodId,
            Amount = 100,
            Currency = "USD",
            Interval = SubscriptionInterval.Month,
            Frequency = 1
        };
    }
    
    [Fact]
    public void CreateSubscription()
    {
        
        var svcPaymentMethod = new xpanse.sdk.PaymentMethod();
        var newPaymentMethod = GetNewPaymentMethod(); 
        newPaymentMethod.Metadata = new Dictionary<string, string>{ { "merchant_id", "value1" } };
        var resultPaymentMethod = svcPaymentMethod.CreatePaymentMethodWithCard(newPaymentMethod);
        
        var svc = new xpanse.sdk.Subscription();
        var result = svc.CreateSubscription(GetNewSubscription(resultPaymentMethod.PaymentMethodId));

        Assert.NotNull(result);
        Assert.Equal(result.PaymentMethodId, resultPaymentMethod.PaymentMethodId);
        Assert.Equal(xpanse.sdk.Models.Subscriptions.Subscription.SubscriptionStatus.Active, result.Status);
    }
    
    [Fact]
    public void GetSubscription()
    {
        var svcPaymentMethod = new xpanse.sdk.PaymentMethod();
        var newPaymentMethod = GetNewPaymentMethod(); 
        newPaymentMethod.Metadata = new Dictionary<string, string>{ { "merchant_id", "value1" } };
        var resultPaymentMethod = svcPaymentMethod.CreatePaymentMethodWithCard(newPaymentMethod);
        
        var svc = new xpanse.sdk.Subscription();
        var subscription = svc.CreateSubscription(GetNewSubscription(resultPaymentMethod.PaymentMethodId));
        
        var resultSubscription = svc.GetSubscription(subscription.SubscriptionId);
        
        Assert.NotNull(resultSubscription);
        Assert.Equal(resultSubscription.PaymentMethodId, resultPaymentMethod.PaymentMethodId);
        Assert.Equal(xpanse.sdk.Models.Subscriptions.Subscription.SubscriptionStatus.Active, resultSubscription.Status);
    }
    
    [Fact]
    public void DeleteSubscription()
    {
        var svcPaymentMethod = new xpanse.sdk.PaymentMethod();
        var newPaymentMethod = GetNewPaymentMethod(); 
        newPaymentMethod.Metadata = new Dictionary<string, string>{ { "merchant_id", "value1" } };
        var resultPaymentMethod = svcPaymentMethod.CreatePaymentMethodWithCard(newPaymentMethod);
        
        var svc = new xpanse.sdk.Subscription();
        var subscription = svc.CreateSubscription(GetNewSubscription(resultPaymentMethod.PaymentMethodId));
        
        var resultSubscription = svc.DeleteSubscription(subscription.SubscriptionId);
        
        Assert.NotNull(resultSubscription);
        Assert.Equal(resultSubscription.PaymentMethodId, resultPaymentMethod.PaymentMethodId);
        Assert.Equal(xpanse.sdk.Models.Subscriptions.Subscription.SubscriptionStatus.Cancelled, resultSubscription.Status);
    }
    
    [Fact]
    public void SearchSubscription()
    {
        var svcPaymentMethod = new xpanse.sdk.PaymentMethod();
        var newPaymentMethod = GetNewPaymentMethod(); 
        newPaymentMethod.Metadata = new Dictionary<string, string>{ { "merchant_id", "value1" } };
        var resultPaymentMethod = svcPaymentMethod.CreatePaymentMethodWithCard(newPaymentMethod);
        
        var svc = new xpanse.sdk.Subscription();
        var newSubscription = GetNewSubscription(resultPaymentMethod.PaymentMethodId);
        newSubscription.Amount = 200;
        var subscriptionListBefore = svc.SearchSubscription(new SubscriptionSearch(){ AmountGreaterThan = 150});
        var subscription = svc.CreateSubscription(newSubscription);
        
        var subscriptionListAfter = svc.SearchSubscription(new SubscriptionSearch(){ AmountGreaterThan = 150});
        
        Assert.NotNull(subscriptionListAfter);
        // The more we run this test, the bigger count is - we need compare before/after count of subscriptions
        Assert.Equal(subscriptionListAfter.Count, subscriptionListBefore.Count + 1);
        Assert.Equal(subscriptionListAfter.Subscriptions.FirstOrDefault(
            x => x.SubscriptionId == subscription.SubscriptionId)?.SubscriptionId, subscription.SubscriptionId);
        
    }
    
    [Fact]
    public void UpdateSubscription()
    {
        
        var svcPaymentMethod = new xpanse.sdk.PaymentMethod();
        var newPaymentMethod = GetNewPaymentMethod(); 
        newPaymentMethod.Metadata = new Dictionary<string, string>{ { "merchant_id", "value1" } };
        var resultPaymentMethod = svcPaymentMethod.CreatePaymentMethodWithCard(newPaymentMethod);
        
        var svc = new xpanse.sdk.Subscription();
        var result = svc.CreateSubscription(GetNewSubscription(resultPaymentMethod.PaymentMethodId));

        Assert.Equal(100, result.Amount);
        Assert.Equal("USD", result.Currency);
        
        result = svc.UpdateSubscription(result.SubscriptionId, new SubscriptionUpdate()
        {
            Amount = 200,
            Currency = "AUD",
            Frequency = 1,
        });
        
        Assert.NotNull(result);
        Assert.Equal(200, result.Amount);
        Assert.Equal("AUD", result.Currency);
        Assert.Equal(1, result.Frequency);
        Assert.Equal(xpanse.sdk.Models.Subscriptions.Subscription.SubscriptionInterval.Day, result.Interval);
        Assert.Null(result.EndAfter);
        Assert.Null(result.Retry);
        Assert.Null(result.Webhook);
    }
    
    [Fact]
    public void PauseSubscription()
    {
        
        var svcPaymentMethod = new xpanse.sdk.PaymentMethod();
        var newPaymentMethod = GetNewPaymentMethod(); 
        var resultPaymentMethod = svcPaymentMethod.CreatePaymentMethodWithCard(newPaymentMethod);
        
        var svc = new xpanse.sdk.Subscription();
        var subscription = svc.CreateSubscription(GetNewSubscription(resultPaymentMethod.PaymentMethodId));
        var result = svc.UpdateSubscriptionStatus(subscription.SubscriptionId, new UpdateSubscriptionStatus()
        {
            Status = xpanse.sdk.Models.Subscriptions.Subscription.SubscriptionStatus.Suspended
        });

        Assert.NotNull(result);
        Assert.Equal(xpanse.sdk.Models.Subscriptions.Subscription.SubscriptionStatus.Suspended, result.Status);
    }

    [Fact]
    public void ReactivateSubscription()
    {
        
        var svcPaymentMethod = new xpanse.sdk.PaymentMethod();
        var newPaymentMethod = GetNewPaymentMethod(); 
        var resultPaymentMethod = svcPaymentMethod.CreatePaymentMethodWithCard(newPaymentMethod);
        
        var svc = new xpanse.sdk.Subscription();
        var subscription = svc.CreateSubscription(GetNewSubscription(resultPaymentMethod.PaymentMethodId));
        svc.UpdateSubscriptionStatus(subscription.SubscriptionId, new UpdateSubscriptionStatus()
        {
            Status = xpanse.sdk.Models.Subscriptions.Subscription.SubscriptionStatus.Suspended
        });
        var result = svc.UpdateSubscriptionStatus(subscription.SubscriptionId, new UpdateSubscriptionStatus()
        {
            Status = xpanse.sdk.Models.Subscriptions.Subscription.SubscriptionStatus.Active
        });

        Assert.NotNull(result);
        Assert.Equal(xpanse.sdk.Models.Subscriptions.Subscription.SubscriptionStatus.Active, result.Status);
    }
}