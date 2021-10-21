using System;
using System.Collections.Generic;
using System.Text;

namespace Technovert.BankApp.Models
{
    public class Customer
    {
        public string AccountId { get; set; }
        public string Name { get; set; }
        public CustomerGender Gender { get; set; }
        public string AccountNumber { get; set; }
        public int Pin { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Passbook { get; set; }
        public AccountStatus Status { get; set; }


        public Customer(string name, CustomerGender gender, int accPin, decimal balance, List<Transaction> passbook, AccountStatus status = AccountStatus.Active)
        {
            this.Name = name;
            this.Gender = gender;
            this.Pin = accPin;
            this.Balance = balance;
            this.Passbook = passbook;
            this.Status = status;
        }


    }
}
