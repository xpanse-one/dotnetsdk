using Newtonsoft.Json;
using xpanse.sdk.Tools;

namespace xpanse.sdk.Models.Subscriptions
{

    public class SubscriptionRetryPolicy
    {
        public int? Maximum { get; set; }
        [JsonConverter(typeof(EnumToStringConverter<SubscriptionRetryInterval>))]
        public SubscriptionRetryInterval Interval { get; set; }
        public int? Frequency { get; set; }
    }
}