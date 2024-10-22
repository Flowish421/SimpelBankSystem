// Program.cs
using System;
using SimpleBankSystem.Services;

namespace SimpleBankSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            BankService bankService = new BankService();
            bankService.Start();
        }
    }
}
