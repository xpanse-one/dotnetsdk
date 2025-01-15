using System.Threading.Tasks;
using xpanse.sdk.Models;

namespace xpanse.sdk
{
    public interface IWebhookSubscription
    {
        Task<WebhookSubscriptionData> CreateWebhookSubscriptionAsync(NewWebhookSubscription newWebhookSubscription);
        WebhookSubscriptionData CreateWebhookSubscription(NewWebhookSubscription newWebhookSubscription);
        Task<WebhookSubscriptionData> GetWebhookSubscriptionAsync(string webhookSubscriptionId);
        WebhookSubscriptionData GetWebhookSubscription(string webhookSubscriptionId);
        Task<WebhookSubscriptionData> DeleteWebhookSubscriptionAsync(string webhookSubscriptionId);
        WebhookSubscriptionData DeleteWebhookSubscription(string webhookSubscriptionId);
        Task<WebhookSubscriptionSearchResults> SearchWebhookSubscriptionAsync(WebhookSubscriptionSearch search);
        WebhookSubscriptionSearchResults SearchWebhookSubscription(WebhookSubscriptionSearch search);
    }
}