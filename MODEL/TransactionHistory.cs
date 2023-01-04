using System;
using System.Collections.Generic;
using System.Text;

namespace BankApk.MODEL
{
    public class TransactionHistory
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string Sender { get; set; }
        public string TransactionDescriptioon { get; set; }
        public string RecieversAccountName { get; set; }
        public string RecieversAccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string TransactionDate { get; set; }

        private int Count = 1;

        public TransactionHistory()
        {
            Id = Count++;
            TransactionDate = DateTime.Now.ToString();
        }
    }
}
