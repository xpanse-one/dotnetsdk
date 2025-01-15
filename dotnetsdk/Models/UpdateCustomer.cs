using System;

namespace xpanse.sdk.Models
{
    public class UpdateCustomer : CustomerEmailAndPhoneData
    {
        public Address Address { get; set; }
        public string DefaultPaymentMethodId { get; set; }
    }
}
