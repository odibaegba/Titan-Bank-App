using System;
using System.Collections.Generic;
using System.Text;

namespace BankApk.MODEL
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }

        public List<Account> Account { get; set; }

        private static int Count = 1;
        public Customer()
        {
            Id = Count++;
            DateCreated = DateTime.Now;
            Account = new List<Account>();
        }

    }
}
