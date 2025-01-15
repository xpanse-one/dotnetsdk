using System.Collections.Generic;

namespace xpanse.sdk.Models
{
    public class NewCustomerProviderToken : NewCustomerEmailAndPhoneData
    {
        public string ProviderId { get; set; }
        public string ProviderToken { get; set; }
        public Dictionary<string,string> ProviderTokenData { get; set; }
        public bool Verify { get; set; }
        public Dictionary<string,string> MetaData { get; set; }
    }
}
