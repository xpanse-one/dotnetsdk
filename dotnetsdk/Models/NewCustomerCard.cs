﻿using System;
using System.Collections.Generic;

namespace xpanse.sdk.Models
{
    public class NewCustomerCard : NewCustomerEmailAndPhoneData
    {
        public string ProviderId { get; set; }
        public CardRequestInformation PaymentInformation { get; set; }
        public bool VaultCard { get; set; }
        public DateTime? VaultExpireDate { get; set; }
        public int? VaultExpireSeconds { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public bool SkipExpiryDateValidation { get; set; }
    }
}
