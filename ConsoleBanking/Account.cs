using System;
using System.Threading;
using System.Collections.Generic;
using System.Globalization;

namespace ConsoleBanking
{
    /// <summary>
    /// Enum <c>AccountType</c> Shows a list of the  Account Types available to the User.
    /// </summary>
    enum AccountType
    {
        Savings,
        Current,
        Checking
    }


    delegate void TrasnactionDelegate(string description, decimal amount);


    /// <summary>
    ///  Class <c>Account</c> Models basic customer information
    ///  tied to a bank account.
    /// </summary>
    /// <param name="_newAccountCreated"> Creates a public object of  account created</param>
    /// <param name="allTransactions"> Creates a list of all transactions performed by USER</param>
    /// <param name="AccountType"> Choose account type from the Enum List</param>
    /// <param name="Balance"> Returns balance of Account </param>

    internal class Account
    { 
        private readonly string _firstName;
        private readonly string _lastName;
        private static readonly Random _accountNumberSeed = new Random(1234567890);
        public decimal Contact { get; set; }
        public string Address { get; set; }
        public DateTime DateCreated { get; }
        public DateTime TimeCreated { get; }
        public string AccountOwner { get => $"{_firstName} {_lastName}"; }
        public int AccountNumber { get; }
        public static AccountType ChooseType { get; set; }

        public static Account _newAccountCreated;

        public static List<Transactions> allTransactions = new List<Transactions>();


        public Account() { }
        public Account(string firstName, string lastName, decimal phoneNumber, string address, decimal openingAmount)
        {
            this._firstName = firstName;
            this._lastName = lastName;
            Contact = phoneNumber;

            Transactions.MakeDeposit("Initial deposit", openingAmount);

            DateCreated = DateTime.Now.ToLocalTime();
            TimeCreated = DateTime.Now.ToLocalTime();

            Address = address;
            AccountNumber = _accountNumberSeed.Next();

        }

        /// <summary>
        /// 
        /// </summary>
        public static decimal Balance
        {
            get
            {
                decimal balance = 0;

                foreach (var item in allTransactions)
                {

                    if (item.TransactionStatus == "Sucessful")
                        balance += item.TransactionAmount;

                    if (item.TransactionStatus == "Unsucessful")
                        balance -= item.TransactionAmount - item.TransactionAmount;

                }

                return balance;
            }
        }


        /// <summary>
        /// Account opening form.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void AccountCreationForm()
        {
            Console.WriteLine("Fill in the following details.");
            Console.WriteLine("Press Enter Key after each field.\n\n");

            Console.Write("Firstname: ");
            string firstName = Console.ReadLine();

            Console.Write("Lastname: ");
            string lastName = Console.ReadLine();


            Console.Write("PhoneNumber: ");
            string number = Console.ReadLine();

            ValidateTransactionInput(number, out decimal contact);

            Console.Write("Address: ");
            string address = Console.ReadLine();

            Console.Write("Opening amount: $");
            string deposit = Console.ReadLine();

            ValidateTransactionInput(deposit, out decimal initialDeposit, "Opening amount: $");

            Console.WriteLine("\nAccount type\n");
            Console.WriteLine("1> Savings 2> Current 3> Checking");

            ConsoleKeyInfo userAccChoice = Console.ReadKey();
            Console.Clear();

            ChooseType = userAccChoice.Key switch
            {
                ConsoleKey.NumPad1 => AccountType.Savings,
                ConsoleKey.NumPad2 => AccountType.Current,
                ConsoleKey.NumPad3 => AccountType.Checking,
                _ => throw new InvalidOperationException(),
            };

            Account newAccount = new Account(firstName, lastName, contact, address, initialDeposit);

            _newAccountCreated = newAccount;

            Console.WriteLine("\t\t\t\t\tAccount sucessfully created.\n\n");
            Dashboard.ReturnToMenu();
        }


        public void Deposit()
        {
            Transactions.WaitWindow();

            Console.Write("Description: ");
            string description = Console.ReadLine();

            Console.Write("Amount:  $");
            string amount = Console.ReadLine();

            ValidateTransactionInput(amount, out decimal depositAmount, "Amount:  $");

            Transactions.MakeDeposit(description, depositAmount);

            Console.WriteLine(Transactions.TransactionReceipt());

            Console.WriteLine("\n\n");
            Dashboard.ReturnToMenu();
        }

        public void Withdraw()
        {
            Transactions.WaitWindow();

            Console.Write("Description: ");
            string description = Console.ReadLine();

            Console.Write("Amount:  $");
            string amount = Console.ReadLine();

            ValidateTransactionInput(amount, out decimal withdrawAmount,"Amount:  $");

            Transactions.MakeWithdrawal(description, withdrawAmount);

            Console.WriteLine(Transactions.TransactionReceipt());

            Console.WriteLine("\n\n");
            Dashboard.ReturnToMenu();
        }


        public void AccountBalance()
        {
            Transactions.WaitWindow();
            Console.WriteLine($"The balances on this account as at {DateTime.Now} are as follows.\n");

            Console.WriteLine($"Current Balance\t\t:{Balance.ToString("C", CultureInfo.CurrentUICulture)}");
            Console.WriteLine($"Available Balance\t:{Balance.ToString("C", CultureInfo.CurrentUICulture)}");
        }



        /// <summary>
        /// Validates input when  either Opening an account, making a  Deposit or Withdrawal 
        /// </summary>
        /// <param name="valueEntered">Amount in string</param>
        /// <param name="amount">Actual Transaction amount</param>
        /// <param name="literalValue">String literal value</param>
        public void ValidateTransactionInput(string valueEntered, out decimal amount, string literalValue = "")
        {
            while (!decimal.TryParse(valueEntered, out amount))
            {
                Console.Clear();
                Console.WriteLine("Wrong Input!\nRe-Enter Amount.");
                Thread.Sleep(1500);

                Console.Clear();
                Console.Write(literalValue);

                valueEntered = Console.ReadLine();
            }
        }



        /// <summary>
        /// 
        /// </summary>
        public void TransactionType(Transactions method)
        {
            Transactions.WaitWindow();

            Console.Write("Description: ");
            string description = Console.ReadLine();

            Console.Write("Amount:  $");
            string amount = Console.ReadLine();

            ValidateTransactionInput(amount, out decimal depositAmount, "Amount:  $");       

            Transactions.MakeDeposit(description, depositAmount);

            Console.WriteLine(Transactions.TransactionReceipt());

            Console.WriteLine("\n\n");
            Dashboard.ReturnToMenu();
        }
    }
}

