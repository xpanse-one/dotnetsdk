using System.Threading.Tasks;
using xpanse.sdk.Models.Subscriptions;

namespace xpanse.sdk
{
    public interface ISubscription
    {
        Task<xpanse.sdk.Models.Subscriptions.Subscription> CreateSubscriptionAsync(SubscriptionCreate subscriptionCreate);
        xpanse.sdk.Models.Subscriptions.Subscription CreateSubscription(SubscriptionCreate subscriptionCreate);
        Task<xpanse.sdk.Models.Subscriptions.Subscription> GetSubscriptionAsync(string subscriptionId);
        xpanse.sdk.Models.Subscriptions.Subscription GetSubscription(string subscriptionId);
        Task<xpanse.sdk.Models.Subscriptions.Subscription> DeleteSubscriptionAsync(string subscriptionId);
        xpanse.sdk.Models.Subscriptions.Subscription DeleteSubscription(string subscriptionId);
        Task<SubscriptionList> SearchSubscriptionAsync(SubscriptionSearch search);
        SubscriptionList SearchSubscription(SubscriptionSearch search);
        Task<xpanse.sdk.Models.Subscriptions.Subscription> UpdateSubscriptionStatusAsync(string subscriptionId, UpdateSubscriptionStatus updateSubscriptionStatus);
        xpanse.sdk.Models.Subscriptions.Subscription UpdateSubscriptionStatus(string subscriptionId, UpdateSubscriptionStatus updateSubscriptionStatus);
        xpanse.sdk.Models.Subscriptions.Subscription UpdateSubscription(string subscriptionId,
            SubscriptionUpdate subscriptionUpdate);
        Task<xpanse.sdk.Models.Subscriptions.Subscription> UpdateSubscriptionAsync(string subscriptionId,
            SubscriptionUpdate subscriptionUpdate);
    }
}