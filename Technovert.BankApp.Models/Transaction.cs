using System;
using System.Collections.Generic;
using System.Text;

namespace Technovert.BankApp.Models
{
    public class Transaction
    {
        public string Id { get; set; }

        public TransactionType type { get; set; }

        public DateTime timeStamp { get; set; }

        public string SenderAccountNumber { get; set; }

        public decimal amount { get; set; }

        public Transaction(TransactionType type, string accountNumber, decimal Amount, DateTime time)
        {
            this.type = type;
            this.SenderAccountNumber = accountNumber;
            this.amount = Amount;
            this.timeStamp = time;
        }
    }
}
