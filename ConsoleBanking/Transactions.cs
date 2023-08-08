﻿using System;
using System.Data;
using System.Threading;
using System.Globalization;
using ConsoleBankDataAccess;
using static ConsoleBanking.Login;

namespace ConsoleBanking
{

    /// <summary>
    /// Represents the various functionality available to the application user.
    /// </summary>
    internal static class Transactions
    {
        /// <summary>
        /// Database instance.
        /// </summary>
        private static readonly DataLayer databaseAccess = new DataLayer();
        private static readonly string sqlStatement = $"Select * From Customer Where Username = '{UserLoggedIn.UserName}'";

        /// <summary>
        /// User's provide basic information and set account preference by filling a form..
        /// </summary>
        public static void OpenAccount()
        {
            Account.GetUserDetails();
            Account.CreateAccount();
            Account.CreateInitialDeposit();
        }

        /// <summary>
        /// Displays user account balance at a specific date and time.
        /// </summary>
        public static void CheckBalance()
        {
            Designs.CenterTextNewLine("\n\n\n\n");

            if (databaseAccess.GetUser(UserLoggedIn, sqlStatement))
            {
                TransactionNotifications.InProgress();
                Console.WriteLine($"The balances on this account as at {DateTime.Now} are as follows.\n");
                Console.WriteLine($"Current Balance\t\t:{UserLoggedIn.Balance.ToString("C", CultureInfo.CurrentUICulture)}");
                Console.WriteLine($"Available Balance\t:{UserLoggedIn.Balance.ToString("C", CultureInfo.CurrentUICulture)}\n\n");
                Designs.DrawLine();
                Console.BackgroundColor = ConsoleColor.Black;
                Menu.ReturnToMenu();
            }
        }
        /// <summary>
        /// Various steps involved in making a withdrawal.
        /// Notifies user if transaction was successfull or not.
        /// Also keeps an history of the transaction.
        /// </summary>
        /// <param name="user">Current user logged in</param>
        public static void MakeWithdrawal()
        {
            Designs.CenterTextNewLine("\n\n\n\n");

            if (databaseAccess.GetUser(UserLoggedIn, sqlStatement))
            {
                TransactionNotifications.InProgress();
                Console.Write("Description: ");
                string description = Console.ReadLine();
                Console.Write("Amount:  $");
                decimal amount;

                while (!(decimal.TryParse(Console.ReadLine(), out amount)))
                {
                    Console.Clear();              
                    Designs.CenterTextNewLine("Wrong Input!");
                    Designs.CenterTextNewLine("Enter a valid Amount");
                    Thread.Sleep(2000);
                    Console.Clear();
                    Console.Write("Amount:  #");
                }
                var withdraw = new TransactionModel { TransactionDescription = description, TransactionAmount = amount };

                if (amount > UserLoggedIn.Balance)
                {
                    TransactionNotifications.InProgress();
                    Designs.CenterTextNewLine("Insufficient funds!\n");
                    Thread.Sleep(2000);
                    Console.Clear();
                    TransactionNotifications.Unsuccessfull();
                    withdraw.TransactionStatus = TransactionStatus.Unsucessfull;
                    withdraw.TransactionType = TransactionType.Debit;
                }
                else if (amount <= 0)
                {
                    TransactionNotifications.InProgress();
                    Designs.CenterTextNewLine("Withdraw amount must be positive.\n");
                    Thread.Sleep(2000);
                    Console.Clear();
                    TransactionNotifications.Unsuccessfull();
                    withdraw.TransactionStatus = TransactionStatus.Unsucessfull;
                    withdraw.TransactionType = TransactionType.Debit;
                }
                else
                {
                    TransactionNotifications.InProgress();
                    TransactionNotifications.Successfull();
                    withdraw.TransactionStatus = TransactionStatus.Sucessfull;
                    withdraw.TransactionType = TransactionType.Debit;
                    UserLoggedIn.Balance -= amount;
                    databaseAccess.UpdateBalance(UserLoggedIn, UserLoggedIn.Balance);
                }
                databaseAccess.CreateTransaction(withdraw, UserLoggedIn.UserName);
                Console.WriteLine(TransactionReceipt.GetReceipt(withdraw) + "\n\n");
                Designs.DrawLine();
                Console.BackgroundColor = ConsoleColor.Black;
                Menu.ReturnToMenu();
            }
        }
        /// <summary>
        /// Various steps involved in making a withdrawal.
        /// Notifies user if transaction was successfull or not.
        /// Also keeps an history of the transaction.
        /// </summary>
        /// <param name="user">Current user logged in</param>
        public static void MakeDeposit()
        {
            Designs.CenterTextNewLine("\n\n\n\n");

            if (databaseAccess.GetUser(UserLoggedIn, sqlStatement))
            {
                TransactionNotifications.InProgress();
                Console.Write("Description: ");
                string description = Console.ReadLine();
                Console.Write("Amount:  #");
                decimal amount;

                while (!(decimal.TryParse(Console.ReadLine(), out amount)))
                {
                    Console.Clear();
                    Designs.CenterTextNewLine("Wrong Input!");
                    Designs.CenterTextNewLine("Enter a valid Amount");
                    Thread.Sleep(2000);
                    Console.Write("Description: ");
                }
                var deposit = new TransactionModel { TransactionDescription = description, TransactionAmount = amount };

                if (amount <= 0)
                {
                    Console.Clear();
                    TransactionNotifications.InProgress();
                    Designs.CenterTextNewLine("Amount of deposit must be positive.\n");
                    Thread.Sleep(2000);
                    Console.Clear();
                    TransactionNotifications.Unsuccessfull();
                    deposit.TransactionStatus = TransactionStatus.Unsucessfull;
                    deposit.TransactionType = TransactionType.Credit;
                }
                else
                {
                    TransactionNotifications.InProgress();
                    TransactionNotifications.Successfull();
                    deposit.TransactionStatus = TransactionStatus.Sucessfull;
                    deposit.TransactionType = TransactionType.Credit;
                    UserLoggedIn.Balance += amount;
                    databaseAccess.UpdateBalance(UserLoggedIn, UserLoggedIn.Balance);
                }
                databaseAccess.CreateTransaction(deposit, UserLoggedIn.UserName);
                Console.WriteLine(TransactionReceipt.GetReceipt(deposit) + "\n\n");
                Designs.DrawLine();
                Console.BackgroundColor = ConsoleColor.Black;
                Menu.ReturnToMenu();
            }
        }
        /// <summary>
        /// displays a valid user's account information
        /// </summary>
        /// <param name="user">Current user logged in</param>
        public static void ViewAccountDetails()
        {
            // Designs.CenterTextNewLine("\n\n\n\n");           
            if (databaseAccess.GetUser(UserLoggedIn, sqlStatement))
            {
                TransactionNotifications.InProgress();
                Console.WriteLine($"Account Name: {UserLoggedIn.FirstName} {UserLoggedIn.LastName}\nAccount Number: {UserLoggedIn.AccountNumber}\nAccount Type: {UserLoggedIn.AccountType}\nEmail: {UserLoggedIn.Email}\nAccount Balance: {UserLoggedIn.Balance.ToString("C", CultureInfo.CurrentUICulture)}\nDate Opened:{UserLoggedIn.DateCreated.ToShortDateString()}\nTime Opened: {UserLoggedIn.TimeCreated}\n\n");
            }
            Designs.DrawLine();
            Console.BackgroundColor = ConsoleColor.Black;
            Menu.ReturnToMenu();
        }
        /// <summary>
        /// Display transaction history of a valid user.
        /// The screen size is adjusted to accommodate the displayed information
        /// Data is displayed as a table
        /// </summary>
        public static void ViewTransactionHistory()
        {
            Designs.CenterTextNewLine("\n\n\n\n");
            TransactionNotifications.InProgress();
            DataTable transactionTable = databaseAccess.GetTransactionHistory(UserLoggedIn.UserName);

            var width = Console.LargestWindowWidth;
            var heigt = Console.LargestWindowHeight;
            Console.SetWindowSize(width, heigt);

            DisplayHistoryAsTable(transactionTable);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("\n\n");
            Menu.ReturnToMenu();

            static void DisplayHistoryAsTable(DataTable table)
            {
                for (int curCol = 0; curCol < table.Columns.Count; curCol++)
                {
                    Console.Write($"{table.Columns[curCol].ColumnName}\t\t");
                }
                Console.WriteLine("\n");
                for (int curRow = 0; curRow < table.Rows.Count; curRow++)
                {
                    for (int curCol = 0; curCol < table.Columns.Count; curCol++)
                    {
                        Console.Write($"{table.Rows[curRow][curCol]}\t");
                    }
                    Console.WriteLine("\n");
                }
            }
        }
    }
}

