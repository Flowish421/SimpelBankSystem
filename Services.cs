// Services/BankService.cs
using System;
using SimpleBankSystem.Models;

namespace SimpleBankSystem.Services
{
    public class BankService
    {
        private readonly BankAccount personkonto;
        private readonly BankAccount sparkonto;
        private readonly BankAccount investeringskonto;
        private readonly string correctPin = "4214"; // Hårdkodad PIN-kod för enkel inloggning

        public BankService()
        {
            personkonto = new BankAccount("P 4214-4163455589", "Flow Flow", "Personkonto", 50000);
            sparkonto = new BankAccount("S 4214-41634124809", "Flow Flow", "Sparkonto", 100000);
            investeringskonto = new BankAccount("I 4214-4985455621", "Flow Flow", "Investeringskonto", 200000);
        }

        public void Start()
        {
            if (!Authenticate()) // Kontrollera om användaren kan logga in
            {
                Console.WriteLine("Felaktig PIN-kod. Programmet avslutas.");
                return;
            }

            bool running = true;

            while (running)
            {
                Console.WriteLine("\nVälj ett alternativ:\n1. Insättning\n2. Uttag\n3. Överföring\n4. Kontrollera saldo\n5. Avsluta");
                switch (Console.ReadLine())
                {
                    case "1":
                        HandleDeposit();
                        break;
                    case "2":
                        HandleWithdraw();
                        break;
                    case "3":
                        HandleTransfer();
                        break;
                    case "4":
                        CheckBalance();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val.");
                        break;
                }
            }
        }

        private bool Authenticate() // Autentiseringsmetod för PIN-kod
        {
            Console.WriteLine("Ange din PIN-kod för att logga in:");
            string inputPin = Console.ReadLine();

            if (inputPin == correctPin)
            {
                Console.WriteLine("Inloggning lyckades.");
                return true;
            }
            else
            {
                Console.WriteLine("Felaktig PIN-kod.");
                return false;
            }
        }

        private BankAccount SelectAccount(string message)
        {
            Console.WriteLine(message + "\n1. Personkonto\n2. Sparkonto\n3. Investeringskonto");
            switch (Console.ReadLine())
            {
                case "1": return personkonto;
                case "2": return sparkonto;
                case "3": return investeringskonto;
                default: Console.WriteLine("Ogiltigt val."); return null;
            }
        }

        private void HandleDeposit()
        {
            BankAccount account = SelectAccount("Välj konto för insättning:");
            if (account != null)
            {
                Console.WriteLine("Ange belopp:");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                    account.Deposit(amount);
                else
                    Console.WriteLine("Ogiltigt belopp.");
            }
        }

        private void HandleWithdraw()
        {
            BankAccount account = SelectAccount("Välj konto för uttag:");
            if (account != null)
            {
                Console.WriteLine("Ange belopp:");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                    account.Withdraw(amount);
                else
                    Console.WriteLine("Ogiltigt belopp.");
            }
        }

        private void HandleTransfer()
        {
            BankAccount fromAccount = SelectAccount("Välj konto att överföra från:");
            if (fromAccount != null)
            {
                BankAccount toAccount = SelectAccount("Välj konto att överföra till:");
                if (toAccount != null && toAccount != fromAccount)
                {
                    Console.WriteLine("Ange belopp:");
                    if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                        fromAccount.Transfer(toAccount, amount);
                    else
                        Console.WriteLine("Ogiltigt belopp.");
                }
            }
        }

        private void CheckBalance()
        {
            BankAccount account = SelectAccount("Välj konto för att kontrollera saldo:");
            if (account != null)
            {
                Console.WriteLine($"Saldo på {account.AccountHolderName} ({account.AccountType}): {account.Balance}");
                Console.WriteLine($"Kontonummer: {account.AccountNumber}");
            }
        }
    }
}
