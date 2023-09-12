using ConsoleBankDataAccess;
using Spectre.Console;
using System;
using System.Threading;
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
            AnsiConsole.Write(new Markup("\n\n\n\n").Centered());

            if (databaseAccess.GetUser(UserLoggedIn, sqlStatement))
            {
                TransactionNotifications.InProgress();
                AnsiConsole.MarkupLine($"[blue]The balances on this account as at[/][red] {DateTime.Now}[/][blue] are as follows.[/]\n");
                AnsiConsole.MarkupLine($"[blue]Current Balance:\t\t[/][red]#{UserLoggedIn.Balance}[/]");
                AnsiConsole.MarkupLine($"[blue]Available Balance:\t[/][red]#{UserLoggedIn.Balance}[/]");
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
            AnsiConsole.Write(new Markup("\n\n\n\n").Centered());

            if (databaseAccess.GetUser(UserLoggedIn, sqlStatement))
            {
                TransactionNotifications.InProgress();
                AnsiConsole.Markup("[blue]Description: [/]");
                Console.ForegroundColor = ConsoleColor.Red;
                string description = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                AnsiConsole.Markup("[blue]Amount: # [/]");
                decimal amount;

                while (!(decimal.TryParse(Console.ReadLine(), out amount)))
                {
                    Console.Clear();
                    AnsiConsole.Write(new Markup("[red]Wrong Input!e[/]\nEnter a valid Amount").Centered()); 
                    Thread.Sleep(2000);
                    Console.Clear();
                    Console.Write("Amount:# ");
                }
                var withdraw = new TransactionModel { TransactionDescription = description, TransactionAmount = amount };

                if (amount > UserLoggedIn.Balance)
                {
                    TransactionNotifications.InProgress();
                    AnsiConsole.Write(new Markup("[red]Insufficient funds!\n[/]").Centered());
                    Thread.Sleep(2000);
                    Console.Clear();
                    TransactionNotifications.ReturnUnsuccessfull();
                    withdraw.TransactionStatus = TransactionStatus.Unsucessfull;
                    withdraw.TransactionType = TransactionType.Debit;
                }
                else if (amount <= 0)
                {
                    TransactionNotifications.InProgress();
                    AnsiConsole.Write(new Markup("[red]Withdraw amount must be positive.\n[/]").Centered()); 
                    Thread.Sleep(2000);
                    Console.Clear();
                    TransactionNotifications.ReturnUnsuccessfull();
                    withdraw.TransactionStatus = TransactionStatus.Unsucessfull;
                    withdraw.TransactionType = TransactionType.Debit;
                }
                else
                {
                    TransactionNotifications.InProgress();
                    TransactionNotifications.ReturnSuccessfull();
                    withdraw.TransactionStatus = TransactionStatus.Sucessfull;
                    withdraw.TransactionType = TransactionType.Debit;
                    UserLoggedIn.Balance -= amount;
                    databaseAccess.UpdateBalance(UserLoggedIn, UserLoggedIn.Balance);
                }
                databaseAccess.CreateTransaction(withdraw, UserLoggedIn.UserName);
                Console.WriteLine(TransactionReceipt.GetReceipt(withdraw) + "\n\n");
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
            AnsiConsole.Write(new Markup("\n\n\n\n").Centered());

            if (databaseAccess.GetUser(UserLoggedIn, sqlStatement))
            {
                TransactionNotifications.InProgress();
                AnsiConsole.Markup("[blue]Description: [/]");
                Console.ForegroundColor = ConsoleColor.Red;
                string description = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                AnsiConsole.Markup("[blue]Amount: # [/]");
                decimal amount;

                while (!(decimal.TryParse(Console.ReadLine(), out amount)))
                {
                    Console.Clear();
                    AnsiConsole.Write(new Markup("[red]Wrong Input!\nEnter a valid Amount[/]").Centered());
                    Thread.Sleep(2000);
                    Console.Write("Description: ");
                }
                var deposit = new TransactionModel { TransactionDescription = description, TransactionAmount = amount };

                if (amount <= 0)
                {
                    Console.Clear();
                    TransactionNotifications.InProgress();
                    AnsiConsole.Write(new Markup("[red]Amount of deposit must be positive.\n[/]").Centered());
                    Thread.Sleep(2000);
                    Console.Clear();
                    TransactionNotifications.ReturnUnsuccessfull();
                    deposit.TransactionStatus = TransactionStatus.Unsucessfull;
                    deposit.TransactionType = TransactionType.Credit;
                }
                else
                {
                    TransactionNotifications.InProgress();
                    TransactionNotifications.ReturnSuccessfull();
                    deposit.TransactionStatus = TransactionStatus.Sucessfull;
                    deposit.TransactionType = TransactionType.Credit;
                    UserLoggedIn.Balance += amount;
                    databaseAccess.UpdateBalance(UserLoggedIn, UserLoggedIn.Balance);
                }
                databaseAccess.CreateTransaction(deposit, UserLoggedIn.UserName);
                Console.WriteLine(TransactionReceipt.GetReceipt(deposit) + "\n\n");
                Menu.ReturnToMenu();
            }
        }

        /// <summary>
        /// displays a valid user's account information
        /// </summary>
        /// <param name="user">Current user logged in</param>
        public static void ViewAccountDetails()
        {       
            if (databaseAccess.GetUser(UserLoggedIn, sqlStatement))
            {
                TransactionNotifications.InProgress();        
                var accountDetails = new Table();
                accountDetails.Title("[blue]\tAccount Details[/]\n\n");
                accountDetails.AddColumns("[blue]Firstname[/]", "[blue]Lastname[/]", "[blue]AccountNumber[/]", "[blue]AccountType[/]", "[blue]Email[/]", "[blue]Balance[/]", "[blue]DateCreated[/]", "[blue]TimeCreated[/]");
                accountDetails.AddRow($"[green]{UserLoggedIn.FirstName}[/]",$"[purple]{UserLoggedIn.LastName}[/]",$"[red]{UserLoggedIn.AccountNumber}[/]",$"{UserLoggedIn.AccountType}",$"[green]{UserLoggedIn.Email}[/]",$"[red]N{UserLoggedIn.Balance}[/]",$"[yellow]{UserLoggedIn.DateCreated.ToShortDateString()}[/]",$"[red]{UserLoggedIn.TimeCreated.ToShortTimeString()}[/]");
                accountDetails.Border(TableBorder.Horizontal);
                AnsiConsole.Write(accountDetails.Centered());      
            }
            Menu.ReturnToMenu();
        }

        /// <summary>
        /// Display transaction history of a valid user.
        /// Data is displayed as a table
        /// </summary>
        public static void ViewTransactionHistory()
        {
            TransactionNotifications.InProgress();
            databaseAccess.GetTransactionHistory(UserLoggedIn.UserName);
            Menu.ReturnToMenu();     
        }
    }
}

