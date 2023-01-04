using System;
using System.Collections.Generic;
using System.Text;
using BankApk.MODEL;
using BankApk.DATA;


namespace BankApk.CORE
{
    public class CustomerRepository
    {
        int accountCount = 1;
        public static string RegisterCustomer(string firstName, string lastName, string email, string address, string phoneNumber, string password)
        {
            string message = string.Empty;
            var oldCustomerCount = DataBase.CustomerTable.Count;
            var foundCustomer = DataBase.CustomerTable.Find(i => i.Email == email);
            if (foundCustomer == null)
            {
                var customer = new Customer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Address = address,
                    PhoneNumber = phoneNumber,
                    Password = password

                };
                DataBase.CustomerTable.Add(customer);

                var updatedCustomerCount = DataBase.CustomerTable.Count;
                if (updatedCustomerCount > oldCustomerCount)
                {
                    message = "Registration Successful";
                }
                else message = "Registration Failed";

            }
            else
            {
                message = "An account with this email already exist";
            }
            return message;
        }

        public string CreatAccount(Account model, Customer customer)
        {
            string message = string.Empty;
            var oldAccountCount = DataBase.AccountTable.Count;
            if (model != null)
            {
                model.Id = accountCount;
                model.CustomerId = customer.Id;
                model.AccountName = customer.FullName;
                customer.Account.Add(model);
                accountCount++;

            }
            DataBase.AccountTable.Add(model);
            int updatedAccountCount = DataBase.AccountTable.Count;
            if (updatedAccountCount > oldAccountCount)
            {
                message = "Account created successfully.";
            }
            else
            {
                message = "Please all field are required.";
            }
            return message;
        }
    }
}
