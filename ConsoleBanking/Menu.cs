using System;
using System.Threading;
using static ConsoleBanking.Login;
using Spectre.Console;

namespace ConsoleBanking
{
    /// <summary>
    /// The various user interface
    /// </summary>
    internal static class Menu
    {
        /// <summary>
        /// 
        /// </summary>
        public static void HomeMenu()
        {     
            var menuItem = AnsiConsole.Prompt(new SelectionPrompt<string>()
           .Title("Welcome...\n\n")
           .PageSize(10)
           .MoreChoicesText("[grey](Move up and down to reveal more items)[/]")
           .AddChoices(new[] {
            "Login for Existing Customers",
            "Open a new Account",
            "About Us",
            "Exit",
           }));
           
            if (menuItem.StartsWith("L"))
            {
                VerifyUser();
            }
            else if (menuItem.StartsWith("O"))
            {
                Transactions.OpenAccount();
            }
            else if (menuItem.StartsWith("A"))
            {
                Console.Clear();
                Console.WriteLine("Coming soon");
            }
            else if (menuItem.StartsWith("E"))
            {
                Console.WriteLine("Thank you for banking with us...");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Thank you for banking with us...");
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void MainMenu()
        {
            Console.SetWindowSize(100, 25);
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            var menuItem = AnsiConsole.Prompt(new SelectionPrompt<string>()
           .Title($"Welcome {UserLoggedIn.UserName}\n\n")
           .PageSize(10)
           .MoreChoicesText("[grey](Move up and down to reveal more items)[/]")
           .AddChoices(new[] {
            "Withdraw ",
            "Deposit",
            "Account Balance",
            "Account Details",
            "Transaction History",
            "Log out"
           }));

            if (menuItem.StartsWith("W"))
            {
                Transactions.MakeWithdrawal();
            }
            else if (menuItem.StartsWith("D"))
            {
                Transactions.MakeDeposit();
            }
            else if (menuItem.Contains("Balance"))
            {
                Transactions.CheckBalance(); ;
            }
            else if (menuItem.Contains("Details"))
            {
                Transactions.ViewAccountDetails();
            }
            else if (menuItem.StartsWith("T"))
            {
                Transactions.ViewTransactionHistory();
                ReturnToMenu();
            }
            else if (menuItem.StartsWith("L"))
            {
                HomeMenu();
            }
            else
            {
                Console.WriteLine("Thank you for banking with us...");
                Environment.Exit(0);
            }
        }

        public static void ReturnToMenu()
        {
            Console.WriteLine("0. Main Menu.");
            ConsoleKeyInfo option = Console.ReadKey();

            if (option.Key == ConsoleKey.NumPad0)
            {
                Console.Clear();
                MainMenu();
            }
            else
            {
                Console.Clear();
                Designs.CenterTextNewLine("Wrong Input!");
                HomeMenu();
            }
        }
    }
}
