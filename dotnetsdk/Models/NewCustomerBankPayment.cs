using System.Collections.Generic;

namespace xpanse.sdk.Models
{
    public class NewCustomerBankPayment : NewCustomerEmailAndPhoneData
    {
        public string ProviderId { get; set; }
        public NewBankPayment BankPaymentInformation { get; set; }
        public Dictionary<string,string> Metadata { get; set; }
    }
}
