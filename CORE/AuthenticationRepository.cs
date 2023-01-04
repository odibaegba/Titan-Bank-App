using BankApk.DATA;
using System;
using System.Collections.Generic;
using System.Text;
using BankApk.MODEL;

namespace BankApk.CORE
{
    public class AuthenticationRepository
    {
        public static bool LogIn(string email, string password)
        {
            bool IsAuthenticated = false;
            foreach (var customer in DataBase.CustomerTable)
            {
                if (customer.Email == email && customer.Password == password)
                {
                    IsAuthenticated = true;
                }
            }
            return IsAuthenticated;
        }

        public bool LogOut(string email, string password)
        {
            return LogIn(email, password) == false;
        }
    }
}
