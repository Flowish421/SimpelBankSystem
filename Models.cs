// Models/BankAccount.cs
using System;

namespace SimpleBankSystem.Models
{
    public class BankAccount
    {
        public string AccountNumber { get; private set; }
        public string AccountHolderName { get; private set; }
        public string AccountType { get; private set; }
        public decimal Balance { get; private set; }

        public BankAccount(string accountNumber, string accountHolderName, string accountType, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            AccountHolderName = accountHolderName;
            AccountType = accountType;
            Balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
            Console.WriteLine($"Insättning genomförd. Nytt saldo: {Balance}");
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
                Console.WriteLine($"Uttag genomfört. Nytt saldo: {Balance}");
            }
            else
            {
                Console.WriteLine("Otillräckligt saldo.");
            }
        }

        public void Transfer(BankAccount toAccount, decimal amount)
        {
            if (amount <= Balance)
            {
                Withdraw(amount);
                toAccount.Deposit(amount);
                Console.WriteLine($"Överföring av {amount} genomförd till {toAccount.AccountHolderName}.");
            }
            else
            {
                Console.WriteLine("Otillräckligt saldo för överföring.");
            }
        }
    }
}
