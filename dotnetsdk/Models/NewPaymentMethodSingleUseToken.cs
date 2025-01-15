using System.Collections.Generic;

namespace xpanse.sdk.Models
{
    public class NewPaymentMethodSingleUseToken 
    {
        public string ProviderId { get; set; }
        public string ProviderToken { get; set; }
        public Dictionary<string,string> ProviderTokenData { get; set; }
        public Dictionary<string,string> Metadata { get; set; }
    }
}