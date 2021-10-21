using System;
using System.Collections.Generic;
using System.Text;

namespace Technovert.BankApp.Models

{
    public enum CustomerType
    {
        NewCustomer = 1,
        ExistingCustomer,
        Exit
    }

    public enum TransactionType
    {
        Deposit = 1,
        Withdraw
    }
    public enum Operations
    {
        Deposit = 1,
        Withdraw,
        Transfer,
        CheckBalance,
        PrintPassbook
    }

    public enum CustomerGender
    {
        Male = 1,
        Female,
        Unknown

    }

    public enum AccountStatus
    {
        Active,
        Closed
    }
}
