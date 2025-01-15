using System;

namespace xpanse.sdk.Models
{
    public class FailureData
    {
        public string ProviderId { get; set; }
        public DateTime DateAttempted { get; set; }
        public string ErrorMessage { get; set; }
        public string GatewayCode { get; set; }
        public string GatewayMessage { get; set; }
        
    }
}