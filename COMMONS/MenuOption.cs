using System;
using System.Collections.Generic;
using System.Text;

namespace BankApk.COMMONS
{
    public class MenuOption
    {
        public static string CreatAccountUI()
        {
            var accountType = string.Empty; //variariable required to creat an account
            while (true)
            {
                Console.WriteLine("Select Account Type");
                var count = 1;
                foreach (var acctype in Enum.GetNames(typeof(Utility.AccountType)))
                {
                    Console.WriteLine($"{count} : {acctype}");
                    count++;
                }
                var response = Console.ReadLine();
                if (response == "1")
                {
                    accountType += Utility.AccountType.Savings;
                    break;
                }
                else if (response == "2")
                {
                    accountType += Utility.AccountType.Current;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalide response");
                    continue;
                }
            }
            return accountType;
        }

        public static string TransactionChannel()
        {
            var transactionChannelType = string.Empty;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Select Transaction Type : ");
                var count = 1;

                foreach (var transchaType in Enum.GetNames(typeof(Utility.TransactionDescription)))
                {
                    Console.WriteLine($"{count} : {transchaType}");
                    count++;
                }
                var response = Console.ReadLine();
                if (response == "1")
                {
                    transactionChannelType += Utility.TransactionDescription.ATM;
                }
                else if (response == "2")
                {
                    transactionChannelType += Utility.TransactionDescription.USSD;
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                    continue;
                }

            }
            return transactionChannelType;
        }



    }
}


