using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Technovert.BankApp.Models;


namespace Technovert.BankApp.Services
{
    public class BankService
    {
        static List<Bank> BankList = new List<Bank>();
        Exceptions error = new Exceptions();

        //Internal Bank services
        private string GenerateBankID(string bankName)
        {
            return (bankName.Substring(0, 4) + DateTime.Today.ToString("dd/MM/yyyy"));
        }
        private string GenerateAccountID(string accountHolderName)
        {
            return (accountHolderName.Substring(0, 4) + DateTime.Today.ToString("dd/MM/yyyy"));
        }
        private string GenerateTransactionID(string bankId, string accountId)
        {
            return ("TXN" + bankId + accountId + DateTime.Today.ToString("dd/MM/yyyy"));
        }
        private string GenerateAccountNumber()
        {
            var random = new Random();
            string accNo = string.Empty;

            for (int i = 0; i < 10; i++)
            {
                accNo = String.Concat(accNo, random.Next(10).ToString());
            }

            return accNo;
        }

        private int GeneratePin()
        {
            Random random = new Random();
            return random.Next(1000, 9999);

        }

        public Bank ValidateBank(string bankName)
        {
            foreach (Bank bank in BankList)
            {
                if (bank.Name == bankName)
                {
                    return bank;
                }
            }

            error.BanknotExist();
            return null;
        }
        public Customer ValidateAccount(Bank bank, string accNumber)
        {
            foreach (Customer user in bank.CustomerDetails)
            {
                if (user.AccountNumber == accNumber)
                {
                    return user;
                }
            }
            error.AccountnotExist();
            return null;
        }
        public bool ValidateAccountPin(Bank bank, string accNumber, int accPin)
        {
            foreach (Customer user in bank.CustomerDetails)
            {
                if (user.Pin == accPin)
                {
                    return true;
                }
            }
            error.WrongPin();
            return false;
        }

        public Bank CreateBank(string bankName, string IFSCCode)
        {
            Bank bank = new Bank(bankName, IFSCCode);
            bank.BankId = GenerateBankID(bankName);
            BankList.Add(bank);
            return bank;
        }

        public Customer CreateAccount(string bankName, string IFSCCode, string name, CustomerGender gender, int balance)
        {
            if (balance < 1000)
            {
                throw new Exception("Account cannot be created!!");
            }
            else
            {
                Bank bank = null;
                foreach (Bank b in BankList)
                {
                    if (b.Name == bankName)
                    {
                        bank = b;
                        break;
                    }
                }
                /*Bank bank = from Bank in BankList
                           where Bank.Name == bankName
                           select Bank;*/
                ///Console.WriteLine(bank);
                if (bank == null)
                {
                    bank = CreateBank(bankName, IFSCCode);
                }
                ///Console.WriteLine(bank.Name);
                string accountNumber = GenerateAccountNumber();
                int accountPin = GeneratePin();
                //List<Transaction> passbook = new List<Transaction>();

                Customer account = new Customer(name, gender, accountNumber, accountPin, balance);
                account.AccountId = GenerateAccountID(name);

                bank.CustomerDetails.Add(account);

                return account;
            }

        }

        //Customer Services
        public void Deposit(Bank bank, Customer accNumber, int amount)
        {
            if (amount > 0)
            {
                accNumber.Balance += amount;
                UpdatePassbook(bank, accNumber, accNumber.AccountNumber, TransactionType.Deposit, amount);
            }
            else
            {
                error.DepositError();
            }
        }
        public void Withdraw(Bank bank, Customer accNumber, int amount)
        {
            if (amount <= GetBalance(accNumber))
            {
                accNumber.Balance -= amount;
                UpdatePassbook(bank, accNumber, accNumber.AccountNumber, TransactionType.Withdraw, amount);
            }
            else
            {
                error.WithdrawError();
            }
        }

        public void Transfer(Bank bank, Customer fromAccNumber, Bank rBank, Customer toAccNumber, int amount)
        {
            if (amount <= GetBalance(fromAccNumber))
            {
                fromAccNumber.Balance -= amount;
                toAccNumber.Balance += amount;

                //BankService Manager = new BankService();
                UpdatePassbook(bank, fromAccNumber, toAccNumber.AccountNumber, TransactionType.Withdraw, amount);
                UpdatePassbook(rBank, toAccNumber, fromAccNumber.AccountNumber, TransactionType.Deposit, amount);
            }
            else
            {
                error.TransferError();
            }
        }


        public decimal GetBalance(Customer accNumber)
        {
            return accNumber.Balance;
        }

        public List<Transaction> PassbookPrint(Customer accNumber)
        {
            return accNumber.Passbook;
        }

        public void UpdatePassbook(Bank bank, Customer toAccount, string fromAccount, TransactionType operation, int amount)
        {
            string transactionId = GenerateTransactionID(bank.Name, toAccount.Name);
            Transaction obj = new Transaction(operation, fromAccount, amount, DateTime.Now);
            obj.Id = transactionId;
            toAccount.Passbook.Add(obj);
        }
    }
}
