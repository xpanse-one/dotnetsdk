using System.Collections.Generic;
using Newtonsoft.Json;
using xpanse.sdk.Tools;

namespace xpanse.sdk.Models
{
    public class NewWebhookSubscription
    {
        public string Url { get; set; }
        public string Authorization { get; set; }
        [JsonConverter(typeof(ListEnumCamelCaseConverter<WebhookType>))]
        public List<WebhookType> Types { get; set; }
    }
}