using System;
using System.Collections.Generic;
using System.Text;
using BankApk.MODEL;
using BankApk.DATA;
using System.Transactions;
using System.Linq;
using BankApk.COMMONS;

namespace BankApk.CORE
{
    public class TransactionRepository
    {
        public string MakeDeposits(decimal amount, string accountNumber, string description)
        {
            string message = string.Empty;
            var previousTransaction = DataBase.TransactionHistoriesTable.Count;
            var newTransaction = new TransactionHistory();

            foreach (var account in DataBase.AccountTable)
            {
                if (account.AccountNumber == accountNumber && amount > 0)
                {
                    newTransaction.AccountId = account.Id;
                    newTransaction.Amount = amount;
                    newTransaction.TransactionDescriptioon = description;
                    newTransaction.Sender = account.AccountName;
                    newTransaction.TransactionType = Utility.TransactionType.Cr.ToString();
                    account.AccountBalance += amount;
                    newTransaction.Balance = account.AccountBalance;
                    account.TransactionHistory.Add(newTransaction);

                }
                DataBase.TransactionHistoriesTable.Add(newTransaction);
                int updatedTransactionCount = DataBase.TransactionHistoriesTable.Count;
                if (updatedTransactionCount > previousTransaction)
                {
                    message = "Transaction Successful";
                }
                else
                {
                    message = "Transaction Failed";
                }

            }
            return message;
        }

        public string SendMoney(decimal amount, int accountId, string recieverAccountNumber, string description)
        {
            string message = string.Empty;
            var previousTransactionCount = DataBase.TransactionHistoriesTable.Count;
            var newTransaction = new TransactionHistory();
            var recieverTransaction = new TransactionHistory();
            foreach (var account in DataBase.AccountTable)
            {
                if (account.Id == accountId)
                {
                    if (account.AccountType == Utility.AccountType.Current.ToString() && account.AccountBalance >= amount) //currentaccount holders can operate a zero account.
                    {
                        newTransaction.AccountId = account.Id;
                        newTransaction.Amount = amount;
                        newTransaction.Sender = account.AccountNumber;
                        newTransaction.RecieversAccountNumber = recieverAccountNumber;
                        newTransaction.TransactionDescriptioon = description;
                        newTransaction.Sender = account.AccountName;
                        newTransaction.TransactionType = Utility.TransactionType.Dr.ToString();
                        account.AccountBalance += amount;
                        newTransaction.Balance = account.AccountBalance;
                        account.TransactionHistory.Add(newTransaction);

                    }
                    else if (account.AccountType == Utility.AccountType.Savings.ToString() && account.AccountBalance >= amount + 1000)
                    {
                        newTransaction.AccountId = account.Id;
                        newTransaction.Amount = amount;
                        newTransaction.Sender = account.AccountNumber;
                        newTransaction.RecieversAccountNumber = recieverAccountNumber;
                        newTransaction.TransactionDescriptioon = description;
                        newTransaction.Sender = account.AccountName;
                        newTransaction.TransactionType = Utility.TransactionType.Dr.ToString();
                        account.AccountBalance += amount;
                        newTransaction.Balance = account.AccountBalance;
                        account.TransactionHistory.Add(newTransaction);
                    }

                    else
                    {
                        return "Insufficient Funds";
                    }

                }
            }

            //Account being creadited 
            foreach (var account in DataBase.AccountTable)
            {
                if (account.AccountNumber == recieverAccountNumber)
                {
                    recieverTransaction.Amount = amount;
                    recieverTransaction.TransactionDescriptioon = description;
                    recieverTransaction.Sender = newTransaction.Sender;
                    recieverTransaction.TransactionType = Utility.TransactionType.Cr.ToString();
                    account.AccountBalance += amount;
                    recieverTransaction.Balance = account.AccountBalance;
                    account.TransactionHistory.Add(newTransaction);

                }
                DataBase.TransactionHistoriesTable.Add(recieverTransaction);
                int updatedTransactionCount = DataBase.TransactionHistoriesTable.Count;
                if (updatedTransactionCount > previousTransactionCount)
                {
                    message = "Transaction Successful";
                }
                else
                {
                    message = "Transaction Failed";
                }
            }

            return message;
        }

        public string TransferToOtherAccount(decimal amount, int accountId, int otherAccountId, string description, string otherDescription)
        {
            string message = string.Empty;
            var previousTransactionCount = DataBase.TransactionHistoriesTable.Count;
            var newTransaction = new TransactionHistory();
            var receiverTransaction = new TransactionHistory();

            foreach (var account in DataBase.AccountTable) // account being debited
            {
                if (account.Id == accountId)
                {
                    //for current account
                    if (account.AccountType == Utility.AccountType.Current.ToString() && account.AccountBalance >= amount)
                    {
                        newTransaction.AccountId = account.Id;
                        newTransaction.Amount = amount;
                        newTransaction.Sender = account.AccountNumber;
                        newTransaction.TransactionDescriptioon = description;
                        newTransaction.TransactionType = Utility.TransactionType.Dr.ToString();
                        account.AccountBalance -= amount;
                        newTransaction.Balance = account.AccountBalance;
                        account.TransactionHistory.Add(newTransaction);

                    }
                    //for savings account
                    else if (account.AccountType == Utility.AccountType.Savings.ToString() && account.AccountBalance >= amount + 1000)
                    {
                        newTransaction.AccountId = account.Id;
                        newTransaction.Amount = amount;
                        newTransaction.Sender = account.AccountNumber;
                        newTransaction.TransactionDescriptioon = description;
                        newTransaction.TransactionType = Utility.TransactionType.Dr.ToString();
                        account.AccountBalance -= amount;
                        newTransaction.Balance = account.AccountBalance;
                        account.TransactionHistory.Add(newTransaction);
                    }
                    else
                    {
                        return "Insufficient Funds";
                    }
                }

            }

            foreach (var account in DataBase.AccountTable)
            {
                if (account.Id == otherAccountId)
                {
                    receiverTransaction.AccountId = account.Id;
                    receiverTransaction.Amount = amount;
                    receiverTransaction.Sender = account.AccountNumber;
                    receiverTransaction.TransactionDescriptioon = otherDescription;
                    receiverTransaction.TransactionType = Utility.TransactionType.Cr.ToString();
                    account.AccountBalance += amount;
                    receiverTransaction.Balance = account.AccountBalance;
                    account.TransactionHistory.Add(newTransaction);

                }
            }
            //transaction records from sender
            DataBase.TransactionHistoriesTable.Add(newTransaction);

            // transaction record of receiving account
            DataBase.TransactionHistoriesTable.Add(receiverTransaction);

            int updatedTransactionCount = DataBase.TransactionHistoriesTable.Count;

            if (updatedTransactionCount > previousTransactionCount)
            {
                message = "Transaction Successful";
            }
            else
            {
                message = "Transaction Not Successful";
            }
            return message;
        }


        public string MakeWithdrawal(decimal amount, int accountId, string description)
        {
            string message = string.Empty;
            var previousTransactionCount = DataBase.TransactionHistoriesTable.Count;
            var newTransaction = new TransactionHistory();

            foreach (var account in DataBase.AccountTable)
            {
                if (account.Id == accountId)
                {
                    if (account.AccountType == Utility.AccountType.Current.ToString() && account.AccountBalance >= amount)
                    {
                        newTransaction.AccountId = account.Id;
                        newTransaction.Amount = amount;
                        newTransaction.TransactionDescriptioon = description;
                        newTransaction.TransactionType = Utility.TransactionType.Dr.ToString();
                        account.AccountBalance -= amount;
                        newTransaction.Balance = account.AccountBalance;
                        account.TransactionHistory.Add(newTransaction);

                    }
                    else if (account.AccountType == Utility.AccountType.Savings.ToString() && account.AccountBalance >= amount + 1000)
                    {
                        newTransaction.AccountId = account.Id; //newTransactionId = transactionCount;
                        newTransaction.Amount = amount;
                        newTransaction.TransactionDescriptioon = description;
                        newTransaction.TransactionType = Utility.TransactionType.Dr.ToString();
                        account.AccountBalance -= amount;
                        newTransaction.Balance = account.AccountBalance;
                        account.TransactionHistory.Add(newTransaction);
                        //transactionCount++;
                    }
                    else
                    {
                        return "Insufficient Funds";
                    }

                }


            }

            DataBase.TransactionHistoriesTable.Add(newTransaction);
            var updatedTrasactionCount = DataBase.TransactionHistoriesTable.Count;
            if (updatedTrasactionCount > previousTransactionCount)
            {
                message = "Transaction Successful";
            }
            else
            {
                message = "Insufficient Funds";
            }

            return message;
        }


        public string GetAccountBalance(int accountId)
        {
            string message = string.Empty;
            foreach (var account in DataBase.AccountTable)
            {
                if (account.Id == accountId)
                {
                    message = $"Your Account Balance is ${account.AccountBalance}";
                }
                else
                {
                    message = "Account don't Exist";
                }
            }
            return message;
        }

        public string GetAccountDetails(string accountNumber)
        {
            string message = string.Empty;
            if (DataBase.AccountTable.Count != 0)
            {
                Console.Clear();
                Table.PrintLine();
                Table.PrintRow("FULL NAME", "ACCOUNT NUMBER", "ACCOUNT TYPE", "ACCOUNT BALANCE");
                Table.PrintLine();

                foreach (var account in DataBase.AccountTable)
                {
                    if (account.AccountNumber == accountNumber)
                    {
                        Table.PrintRow($"{account.AccountName}", $"{account.AccountNumber}", $"{account.AccountType}", $"${account.AccountBalance}");
                        Table.PrintLine();
                    }
                    else
                    {
                        message = "No Account with this Account Number ";
                    }
                }

            }
            return message;
        }

        public string GetStatementOfAccount(int accountId)
        {
            string message = string.Empty;
            if (DataBase.TransactionHistoriesTable.Count != 0)
            {
                Console.Clear();
                Console.WriteLine($"Account Statement On Account Number {DataBase.AccountTable[accountId - 1].AccountNumber}");
                Table.PrintLine();
                Table.PrintRow();
                Table.PrintRow("DATE", "DESCRIPTION", "AMOUNT", "BALANCE");
                Table.PrintLine();

                var transactions = DataBase.AccountTable.FirstOrDefault(account => account.Id == accountId).TransactionHistory;

                foreach (var transaction in transactions)
                {
                    Table.PrintRow($"{transaction.TransactionDate}", $"{transaction.TransactionDescriptioon}", $"{transaction.Amount}", $"{transaction.Balance}");
                    Table.PrintLine();
                }



            }
            else
            {
                message = "No Transaction Yet";
            }
            return message;
        }
    }
}