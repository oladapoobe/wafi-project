using System;
using System.Collections.Generic;
using System.Text;

namespace waficash.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long AccountBalance { get; set; }
        public long AccountNumber { get; set; }
        public string Currency { get; set; }
    }
}
