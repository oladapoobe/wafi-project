using System;
using System.Collections.Generic;
using System.Text;

namespace waficash.Models
{
    public class TransactionInfo
    {
        public long Id { get; set; }
        public decimal Deposit { get; set; }
        public decimal AccountBalance { get; set; }
        public decimal Withdrawal { get; set; }
        public DateTime DateCreated { get; set; }
        public long AccountNumber { get; set; }
        public string Currency { get; set; }


    }
}
