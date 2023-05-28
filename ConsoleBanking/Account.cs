using System;
using System.Threading;
using ConsoleBankDataAccess;
using static ConsoleBanking.AccountInfo;


namespace ConsoleBanking
{
    public class Account
    {
        // Account number Generator
        private static readonly Random _accountNumberSeed = new Random();

        public static void CreateAccount()
        {
            // Create new user and save in Database
            AccountModel newAccount = new AccountModel
            {
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                UserName = username,
                Password = password,
                DateCreated = DateTime.Now.Date,
                Balance = initialDeposit,
                TimeCreated = DateTime.Now.TimeOfDay,
                AccountNumber = _accountNumberSeed.Next(1234567891),
                AccountType = accountType,
                Pin = pin,
            };
            DataAccess newCustomer = new DataAccess();
            newCustomer.CreateCustomerAccount(newAccount);


            //// Create and Save first transaction in Database
            var initialdeposit = new TransactionModel() { TransactionDescription = "Initial Deposit", TransactionAmount = initialDeposit };

            TransactionModel newTrans = new TransactionModel
            {
                TransactionAmount = initialDeposit,
                TransactionDescription = "Initial Deposit",
                TransactionType = TransactionType.Credit,
                TransactionDate = DateTime.Now.Date,
                TransactionTime = DateTime.Now.TimeOfDay,
                TransactionStatus = TransactionStatus.Sucessfull
            };
            newCustomer.CreateTransaction(initialdeposit, username);

            Console.Clear();
            Designs.CenterTextNewLine("Account sucessfully created.\n\n");

            Thread.Sleep(6000);
            Console.Clear();

            // Login
            Menu.HomeMenu();
        }
    }
}
