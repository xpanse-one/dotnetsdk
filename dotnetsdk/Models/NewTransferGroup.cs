using System.Collections.Generic;

namespace xpanse.sdk.Models
{
    public class NewTransferGroup
    {
        public string ProviderId { get; set; }
        public string GroupReference { get; set; }
        public string ChargeId { get; set; }
        public List<NewTransfer> Transfers { get; set; }
    }
}
