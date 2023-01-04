using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace BankApk.COMMONS
{
    public class Checkers
    {
        public static bool ValidateName(string name)
        {
            if (!Regex.IsMatch(name, @"[A-Za-z]")) //Names must only contain strings
                return false;
            return true;
        }

        public static bool ValidateEmail(string email)
        {
            if (!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")) //email must only contain strings
                return false;
            return true;
        }

        public static bool ValidateAdress(string address)
        {
            if (!Regex.IsMatch(address, @"[A-Za-z]")) //Adress must only contain strings
                return false;
            return true;
        }
        public static bool ValidatePassword(string password)
        {
            if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,15}$"))
                return false;
            return true;
        }

        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            if (!Regex.IsMatch(phoneNumber, @"^[0]\d{10}$"))
                return false;
            return true;
        }

        public static bool ValidateTransAccount(string accountNumber)
        {
            if (!Regex.IsMatch(accountNumber, @"\d{10}$"))
                return false;
            return true;
        }
    }
}
