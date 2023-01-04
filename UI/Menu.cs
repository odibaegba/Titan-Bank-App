using BankApk.COMMONS;
using BankApk.DATA;
using BankApk.MODEL;
using BankApk.CORE;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BankApk.UI
{
    public class Menu
    {
        public void RunApp()
        {
            var newCustomer = new CustomerRepository();
            var newTransaction = new TransactionRepository();

            Console.WriteLine("Welcome to Titan Bank.\n  \rWe hope you get the best of Banking experience on our platform. ");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Press Enter to Start or any other Key to Exit");
            Console.WriteLine(">>>>");
            string begin = Console.ReadLine();
            Console.Clear();

            while (string.IsNullOrEmpty(begin))
            {
                Console.WriteLine("Click 1 to Register");
                Console.WriteLine("Click 2 to Login");
                Console.WriteLine("Click 3 to Quit");
                Console.WriteLine(">>>>");

                var choice = Console.ReadLine();
                if (choice == "1")
                {
                    //field for user registeration
                    var firstName = string.Empty;
                    var lastName = string.Empty;
                    var email = string.Empty;
                    var address = string.Empty;
                    var phoneNumber = string.Empty;
                    var password = string.Empty;

                    Console.Clear();

                    Console.WriteLine("Kindly fill in all the fields");

                    while (true)
                    {
                        Console.WriteLine("First Name: ");
                        var input = Console.ReadLine();
                        var response = Checkers.ValidateName(input);
                        Console.Clear();

                        if (response != true)
                        {
                            Console.WriteLine("Invalid Input!");
                            Console.WriteLine("Name can only contain alphabet");
                            Console.WriteLine("");
                            continue;
                        }
                        else
                        {
                            firstName += input;
                            break;
                        }
                    }

                    while (true)
                    {
                        Console.WriteLine("Last Name: ");
                        var input = Console.ReadLine();
                        var response = Checkers.ValidateName(input);
                        Console.Clear();

                        if (response != true)
                        {
                            Console.WriteLine("Invalid Input!");
                            Console.WriteLine("Name can only contain alphabet");
                            Console.WriteLine("");
                            continue;
                        }
                        else
                        {
                            lastName += input;
                            break;
                        }

                    }
                    while (true)
                    {
                        Console.WriteLine("Email: ");
                        var input = Console.ReadLine();
                        var response = Checkers.ValidateEmail(input);
                        Console.Clear();

                        if (response != true)
                        {
                            Console.WriteLine("Invalid Email Adress");
                            Console.WriteLine("");
                            continue;
                        }
                        else
                        {
                            email += input;
                            break;
                        }
                    }

                    while (true)
                    {
                        Console.WriteLine("Adress: ");
                        var input = Console.ReadLine();
                        var response = Checkers.ValidateAdress(input);
                        Console.Clear();

                        if (response != true)
                        {
                            Console.WriteLine("Invalid Address");
                            Console.WriteLine("");
                            continue;
                        }
                        else
                        {
                            address += input;
                            break;
                        }
                    }

                    while (true)
                    {
                        Console.WriteLine("Phone NUmber: ");
                        var input = Console.ReadLine();
                        var response = Checkers.ValidatePhoneNumber(input);
                        Console.Clear();

                        if (response != true)
                        {
                            Console.WriteLine("Invalid Phone Number");
                            Console.WriteLine("");
                            continue;

                        }
                        else
                        {
                            phoneNumber += input;
                            break;
                        }
                    }

                    while (true)
                    {
                        Console.WriteLine($"Hint: Password should be minimum of 6 characters\n" +
                                         $"Should include alphanumeric and at least one special characters (@, #, $, %, ^, &, !)");
                        Console.WriteLine("");
                        Console.WriteLine("Password: ");
                        var input = Console.ReadLine();
                        var respond = Checkers.ValidatePassword(input);
                        Console.Clear();

                        if (respond != true)
                        {
                            Console.WriteLine("Incorrect Password Formart");
                            Console.WriteLine("");
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Confirm Password: ");
                            var input2 = Console.ReadLine().Trim();
                            if (input2 == input)
                            {
                                password += input;
                                Console.Clear();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Password didn't match. please try again.");
                                continue;
                            }
                        }
                    }

                    var registration = CustomerRepository.RegisterCustomer(firstName, lastName, email, address, phoneNumber, password);
                    Console.WriteLine(registration);

                }

                else if (choice == "2")
                {
                    Console.Clear();
                    //making use of login details
                    var email = string.Empty;
                    var password = string.Empty;

                    while (true)
                    {
                        Console.WriteLine("Enter Email: ");
                        var input = Console.ReadLine();
                        var response = Checkers.ValidateEmail(input);
                        Console.Clear();

                        if (response != true)
                        {
                            Console.WriteLine("Invalid Input");
                            Console.WriteLine("");
                            continue;
                        }
                        else
                        {
                            email += input;
                            break;
                        }
                    }
                    while (true)
                    {
                        Console.WriteLine("Enter Password: ");
                        var input = Console.ReadLine();
                        var response = Checkers.ValidatePassword(input);
                        Console.Clear();

                        if (response != true)
                        {
                            Console.WriteLine("Invalid Password");
                            Console.WriteLine("");
                            continue;
                        }
                        else
                        {
                            password += input;
                            break;
                        }

                    }
                    var login = AuthenticationRepository.LogIn(email, password);
                    if (login == true)
                    {
                        foreach (var customer in DataBase.CustomerTable)
                        {
                            if (customer.Email == email)
                            {
                                Console.Clear();
                                Console.WriteLine("Login Successful");
                                while (true)
                                {
                                    Console.WriteLine($"Welcome {customer.FullName}");
                                    Console.WriteLine("");
                                    Console.WriteLine("Press 1 to Creat Account");
                                    Console.WriteLine("Press 2 to Make Deposits");
                                    Console.WriteLine("Press 3 to Make Withdrawals");
                                    Console.WriteLine("Press 4 to Send MOney");
                                    Console.WriteLine("Press 5 to Check Account Balance");
                                    Console.WriteLine("Press 6 to Check Account Information");
                                    Console.WriteLine("Press 7 to Generate Statement Of Account ");
                                    Console.WriteLine("Press 8 to Logout");

                                    var input = Console.ReadLine();

                                    if (input == "1")
                                    {
                                        Console.Clear();
                                        var accountType = MenuOption.CreatAccountUI();
                                        //creating new account instance for the customer
                                        var account = new Account()
                                        {
                                            AccountType = accountType,
                                        };

                                        var message = newCustomer.CreatAccount(account, customer);
                                        Console.Clear();
                                        Console.WriteLine(message);

                                    }
                                    else if (input == "2")
                                    {
                                        Console.Clear();
                                        decimal amount = 0;

                                        while (true)
                                        {
                                            if (customer.Account.Count > 0)
                                            {
                                                int count = 1;
                                                string accNum = "";
                                                var accNumMap = new Dictionary<string, string>();
                                                Console.WriteLine("Select Account to carry out Transasction");
                                                foreach (var account in customer.Account)
                                                {
                                                    Console.WriteLine($"{count}. {account.AccountName} {account.AccountNumber} {account.AccountType}");
                                                    accNumMap.Add(count.ToString(), account.AccountNumber);
                                                    count++;
                                                }
                                                var accId = Console.ReadLine();
                                                if (accNumMap.TryGetValue(accId, out accNum))
                                                {
                                                    Console.Clear();

                                                    while (true)
                                                    {
                                                        Console.WriteLine($"Enter Amount to Deposit: ");
                                                        var amountChoice = Console.ReadLine();
                                                        bool success = decimal.TryParse(amountChoice, out amount);
                                                        if (success)
                                                        {
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Invalid Transaction! Please a Number");
                                                            continue;
                                                        }
                                                    }
                                                    var withdrawChannel = MenuOption.TransactionChannel();
                                                    var deposit = newTransaction.MakeDeposits(amount, accNum, withdrawChannel);
                                                    Console.Clear();
                                                    Console.WriteLine(deposit);
                                                    break;

                                                }
                                                else
                                                {
                                                    Console.WriteLine("You have Choosen an Invalid Option");
                                                    break;
                                                }


                                            }
                                            else
                                            {
                                                Console.WriteLine("You don't have an Account yet. Dpo you wish to creat One?");
                                                Console.WriteLine("1. Creat account");
                                                Console.WriteLine("2. Back");
                                                var userInput = Console.ReadLine();
                                                if (userInput == "1")
                                                {
                                                    Console.Clear();
                                                    var accountType = MenuOption.CreatAccountUI();
                                                    Console.Clear();
                                                    //Creat A NEW Account instance for the customer
                                                    var account = new Account()
                                                    {
                                                        AccountType = accountType,
                                                    };
                                                    var message = newCustomer.CreatAccount(account, customer);
                                                    Console.Clear();
                                                    Console.WriteLine(message);

                                                }
                                                else if (userInput == "2")
                                                {
                                                    break;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid INput");
                                                    continue;
                                                }

                                            }
                                        }
                                    }
                                    else if (input == "3")
                                    {
                                        while (true)
                                        {
                                            Console.Clear();
                                            if (customer.Account.Count > 0)
                                            {
                                                int count = 1;
                                                Console.WriteLine("Select Account to carry out transactions");
                                                foreach (var account in customer.Account)
                                                {
                                                    Console.WriteLine($"{count}. {account.AccountName} {account.AccountNumber} {account.AccountType}");
                                                    count++;

                                                    var accId = Console.ReadLine();
                                                    bool success = int.TryParse(accId, out int accountId);
                                                    //variables needed for withdwaral
                                                    decimal amount;
                                                    if (!success)
                                                    {
                                                        Console.WriteLine("Customer ID must be a Number");
                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine($"Enter Amount to be Withdrawn: ");
                                                        var amountChoice = Console.ReadLine();

                                                        while (true)
                                                        {
                                                            bool isSuccessful = decimal.TryParse(amountChoice, out amount);
                                                            if (isSuccessful)
                                                            {
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Invalid Transaction! Please Enter a Number");
                                                                continue;
                                                            }
                                                        }

                                                        var transactionChannel = MenuOption.TransactionChannel();
                                                        string withdraw = newTransaction.MakeWithdrawal(amount, accountId, transactionChannel);
                                                        Console.Clear();
                                                        Console.WriteLine(withdraw);
                                                        break;
                                                    }
                                                    
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("You don't have an Account yet.Do you wish to creat one?");
                                                Console.WriteLine("1. Creat account");
                                                Console.WriteLine("2. Back");
                                                var userInput = Console.ReadLine();
                                                if (userInput == "1")
                                                {
                                                    Console.Clear();
                                                    var accountType = MenuOption.CreatAccountUI();
                                                    Console.Clear();
                                                    //Creat A NEW Account instance for the customer
                                                    var account = new Account()
                                                    {
                                                        AccountType = accountType,
                                                    };
                                                    var message = newCustomer.CreatAccount(account, customer);
                                                    Console.Clear();
                                                    Console.WriteLine(message);

                                                }
                                                else if (userInput == "2")
                                                {
                                                    break;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid INput");
                                                    continue;
                                                }
                                            }
                                        }
                                    }
                                    else if (input == "4")
                                    {
                                        //variables for transfer
                                        int accountId;
                                        int otherAccountId;
                                        string description = null;
                                        string otherDescription = null;
                                        string recieverAccNumber = string.Empty;
                                        string senderAccNumber = string.Empty;

                                        while (true)
                                        {
                                            if (customer.Account.Count > 0)
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Select Account to carry out transaction");
                                                var count = 1;
                                                foreach (var account in customer.Account)
                                                {
                                                    Console.WriteLine($"{count}. {account.AccountName} {account.AccountNumber} {account.AccountType}");
                                                    count++;
                                                    senderAccNumber = account.AccountNumber;

                                                }
                                                var accId = Console.ReadLine();
                                                bool success = int.TryParse(accId, out accountId);
                                                if (!success)
                                                {
                                                    Console.WriteLine("Customer ID must be a Number");
                                                }
                                                while (true)
                                                {
                                                    Console.WriteLine($"Choose Transfer Channel: ");
                                                    int counts = 1;
                                                    foreach (var transferChannel in Enum.GetNames(typeof(Utility.TransactionDescription)))
                                                    {
                                                        Console.WriteLine($"{count}. {transferChannel}");
                                                        counts++;

                                                    }
                                                    var response = Console.ReadLine();
                                                    if (response == "1")
                                                    {
                                                        description += $"{Utility.TransactionDescription.USSD}transfer";
                                                        otherDescription = $"{Utility.TransactionDescription.USSD}transfered from {senderAccNumber}";
                                                        break;
                                                    }
                                                    else if (response == "2")
                                                    {
                                                        description += $"{Utility.TransactionDescription.ATM}transfer";
                                                        otherDescription = $"{Utility.TransactionDescription.ATM}transfered from {senderAccNumber}";
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Invalid Input. please try again");
                                                        continue;
                                                    }
                                                }
                                                Console.WriteLine("");
                                                Console.WriteLine("******************************************");
                                                Console.WriteLine("Press 1 to transfer to your other accounts");
                                                Console.WriteLine("Press 2 to transfer to another customer");
                                                Console.WriteLine("Press 3 to cancel");
                                                var transferChoice = Console.ReadLine();
                                                decimal amount;
                                                if (transferChoice == "1")
                                                {
                                                    foreach (var account in customer.Account)
                                                    {
                                                        if (customer.Account.Count < 1)
                                                        {
                                                            Console.WriteLine("You do not have any account");
                                                            Console.WriteLine("Press 1 to creat a new account");
                                                            Console.WriteLine("Press 2 to cancel operation ");
                                                            break;

                                                        }
                                                        else if (account.Id != accountId)
                                                        {
                                                            Console.WriteLine("Select Account to transfer to: ");
                                                            Console.WriteLine($"Press {account.Id}. {account.AccountName} {account.AccountNumber} {account.AccountType}");
                                                            var accToTransferTo = Console.ReadLine();
                                                            bool isSuccessful = int.TryParse(accToTransferTo, out otherAccountId);
                                                            if (!isSuccessful)
                                                            {
                                                                Console.WriteLine("Customer ID must be a Number");
                                                                continue;

                                                            }
                                                            while (true)
                                                            {
                                                                Console.WriteLine($"Enter amount to be transferred: ");
                                                                var amountChoice = Console.ReadLine();
                                                                bool response = decimal.TryParse(amountChoice, out amount);
                                                                if (response)
                                                                {
                                                                    break;
                                                                }
                                                                else
                                                                {
                                                                    Console.WriteLine("Invalid Transaction! Please enter a number");
                                                                    continue;
                                                                }
                                                            }
                                                            Console.WriteLine(newTransaction.TransferToOtherAccount(amount, accountId, otherAccountId, description, otherDescription));
                                                            break;
                                                        }
                                                    }
                                                }
                                                else if (transferChoice == "2")
                                                {
                                                    Console.Write($"Enter amount to Transfer: ");
                                                    var amountChoice = Console.ReadLine();

                                                    while (true)
                                                    {
                                                        bool isSuccessful = decimal.TryParse(amountChoice, out amount);
                                                        if (isSuccessful)
                                                            break;
                                                        else
                                                        {
                                                            Console.WriteLine("Invalid transaction! Please enter a number");
                                                            continue;
                                                        }
                                                    }
                                                }
                                            }

                                        }
                                    }

                                }
                            }
                        }


                    }

                }
            }
        }
    }
}



