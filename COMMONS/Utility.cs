using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace BankApk.COMMONS
{
    public class Utility
    {
        public enum AccountType
        {
            Savings,
            Current
        }

        public enum TransactionType
        {
            Cr,
            Dr
        }

        public enum TransactionDescription
        {
            USSD,
            ATM
        }

        public static string GenerateAccount()
        {
            var accountNumber = string.Empty;
            string startWith = "09";
            Random random = new Random();
            string r = random.Next(0, 999999).ToString("D8");
            accountNumber = startWith + r;

            return accountNumber;
        }
    }
}
