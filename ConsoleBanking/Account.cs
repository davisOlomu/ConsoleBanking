using System;
using System.Threading;
using ConsoleBankDataAccess;
using static ConsoleBanking.AccountInformation;

namespace ConsoleBanking
{
    /// <summary>
    /// 
    /// </summary>
    public static class Account
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly Random _accountNumberSeed = new Random();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="initialDeposit"></param>
        /// <param name="type"></param>
        /// <param name="pin"></param>
        public static void CreateAccount(string firstname, string lastname, string email, string username, string password, decimal initialDeposit, AccountType type, int pin)
        {
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
                AccountType = type,
                Pin = pin,
            };
            DataLayer newCustomer = new DataLayer();
            newCustomer.CreateAccount(newAccount);

            var initialdeposit = new TransactionModel() { TransactionDescription = "Initial Deposit", TransactionAmount = initialDeposit };

            TransactionModel newTransaction = new TransactionModel
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

            Menu.HomeMenu();
        }
    }
}
