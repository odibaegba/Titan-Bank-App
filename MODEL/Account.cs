using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using BankApk.COMMONS;

namespace BankApk.MODEL
{
    public class Account
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string AccountName { get; set; }
        public decimal AccountBalance { get; set; }
        private decimal accountBalance = 0;

        public List<TransactionHistory> TransactionHistory { get; set; }

        public Account()
        {
            AccountBalance = accountBalance;
            AccountNumber = Utility.GenerateAccount();
            TransactionHistory = new List<TransactionHistory>();

        }






    }
}
