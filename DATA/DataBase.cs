using System;
using System.Collections.Generic;
using System.Text;
using BankApk.MODEL;

namespace BankApk.DATA
{
    public class DataBase
    {
        public static List<Customer> CustomerTable { get; set; } = new List<Customer>();
        public static List<Account> AccountTable { get; set; } = new List<Account>();
        public static List<TransactionHistory> TransactionHistoriesTable { get; set; } = new List<TransactionHistory>();
    }
}
