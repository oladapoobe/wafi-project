using System;
using System.Collections.Generic;
using System.Text;

namespace waficash.Models
{
    public class Transfer
    {
        public long AccountNumberFrom { get; set; }
        public long AccountNumberTo { get; set; }
        public long Amount { get; set; }
    }
}
