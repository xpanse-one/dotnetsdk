﻿namespace xpanse.sdk.Models
{
    public class NewTransfer
    {
        public string Account { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Message { get; set; }
        public string Reference { get; set; }
    }
}
