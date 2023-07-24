using System;
using System.Threading;
using ConsoleBankDataAccess;
using static ConsoleBanking.Validations;

namespace ConsoleBanking
{
    /// <summary>
    /// Logical steps involved in creating a new Account.
    /// </summary>
    internal class Account
    {
        /// <summary>
        /// 
        /// </summary>
        private static string _firstname;
        private static string _lastname;
        private static string _email;
        private static string _username;
        private static string _password;
        private static int _pin;
        private static AccountType _acctype;
        private static decimal _initialDeposit;
        private static readonly Random _accountNumberSeed = new Random();
        private static readonly DataLayer databaseAccess = new DataLayer();

        /// <summary>
        /// Collects data, validates them and passes 
        /// accurate data, for account creation.
        /// </summary>
        public static void GetUserDetails()
        {
            Designs.CenterTextNewLine("Fill in the following details.");
            Designs.CenterTextNewLine("Press Enter Key after each field.\n\n");
            Thread.Sleep(3000);
            Console.Clear();

            Console.WriteLine("Personal details: \n\n");
            _firstname = ValidateFirstName();
            _lastname = ValidateLastName();
            _email = ValidateEmail();
            Console.Clear();

            Console.WriteLine("Security: \n\n");
            _username = ValidateUsername();
            _password = ValidatePassword();
            _pin = ValidatePin();
            Console.Clear();

            Console.WriteLine("Account details: \n\n ");
            _acctype = ValidateAccoutType();
            _initialDeposit = ValidateInitialDeposit();
        }

        /// <summary>
        /// Create a new User account using details
        /// provided by the user.
        /// </summary>
        public static void CreateAccount()
        {
            AccountModel newAccount = new AccountModel
            {
                FirstName = _firstname,
                LastName = _lastname,
                Email = _email,
                UserName = _username,
                Password = _password,
                DateCreated = DateTime.Now.Date,
                Balance = _initialDeposit,
                TimeCreated = DateTime.Now.TimeOfDay,
                AccountNumber = _accountNumberSeed.Next(1234567890),
                AccountType = _acctype,
                Pin = _pin,
            };
            DataLayer newCustomer = new DataLayer();
            newCustomer.CreateAccount(newAccount);
        }

        /// <summary>
        /// All new accounts must be activated with a minimum deposit
        /// of #1000.
        /// </summary>
        public static void CreateInitialDeposit()
        {
            TransactionModel initialTransaction = new TransactionModel
            {
                TransactionAmount = _initialDeposit,
                TransactionDescription = "Initial Deposit",
                TransactionType = TransactionType.Credit,
                TransactionDate = DateTime.Now.Date,
                TransactionTime = DateTime.Now.TimeOfDay,
                TransactionStatus = TransactionStatus.Sucessfull
            };
            databaseAccess.CreateTransaction(initialTransaction, _username);
            Console.Clear();
            Designs.CenterTextNewLine("Account sucessfully created.\n\n");
            Thread.Sleep(6000);
            Console.Clear();
            Menu.HomeMenu();
        }
    }
}

