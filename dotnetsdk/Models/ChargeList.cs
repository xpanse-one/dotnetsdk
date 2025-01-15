﻿using System.Collections.Generic;

namespace xpanse.sdk.Models
{
    public class ChargeList
    {
        public int Limit { get; set; }
        public int Skip { get; set; }
        public int Count { get; set; }
        public List<ChargeData> Charges { get; set; }
    }
}
