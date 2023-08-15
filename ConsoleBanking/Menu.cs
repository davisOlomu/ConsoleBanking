using Spectre.Console;
using System;
using static ConsoleBanking.Login;

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
            AnsiConsole.Write(
            new FigletText("KH Bank")
           .Centered()
           .Color(Color.Blue));

            string login = "Login".PadLeft(50);
            string newAccount = "Open a new Account".PadLeft(57);
            string about = "About us".PadLeft(52);
            string exit = "Exit".PadLeft(50);
            Console.WriteLine("\n\n");
            var menuItem = AnsiConsole.Prompt(new SelectionPrompt<string>()
           .PageSize(10)
           .MoreChoicesText("[grey](Move up and down to reveal more items)[/]")
           .AddChoices(login)
           .AddChoices(newAccount)
           .AddChoices(about)
           .AddChoices(exit));

            if (menuItem.Contains("Login"))
            {
                VerifyUser();
            }
            else if (menuItem.Contains("Open"))
            {
                Transactions.OpenAccount();
            }
            else if (menuItem.Contains("About"))
            {
                Console.Clear();
                Console.WriteLine("Coming soon");
            }
            else if (menuItem.Contains("Exit"))
            {
                Console.Clear();
                Console.WriteLine("Thank you for banking with us...");
                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                HomeMenu();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void MainMenu()
        {
            AnsiConsole.Write(new Markup("[white]Welcome[/]").Centered());
            AnsiConsole.Write(
            new FigletText($"{UserLoggedIn.UserName}")
           .Centered()
           .Color(Color.Blue));
            Console.WriteLine("\n\n");
            string title = $"Welcome {UserLoggedIn.UserName}".PadLeft(55);
            string withdraw = "Withdraw".PadLeft(50);
            string deposit = "Deposit".PadLeft(50);
            string balance = "Account Balance".PadLeft(54);
            string details = "Account Details".PadLeft(54);
            string history = "Transaction History".PadLeft(56);
            string logout = "Log out".PadLeft(50);

            Console.SetWindowSize(100, 25);
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            var menuItem = AnsiConsole.Prompt(new SelectionPrompt<string>()
           .PageSize(10)
           .MoreChoicesText("[grey](Move up and down to reveal more items)[/]")
           .AddChoices(withdraw)
           .AddChoices(deposit)
           .AddChoices(balance)
           .AddChoices(details)
           .AddChoices(history)
           .AddChoices(logout));

            if (menuItem.Contains("Withdraw"))
            {
                Transactions.MakeWithdrawal();
            }
            else if (menuItem.Contains("Deposit"))
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
            else if (menuItem.Contains("History"))
            {
                Transactions.ViewTransactionHistory();
                ReturnToMenu();
            }
            else if (menuItem.Contains("out"))
            {
                Console.Clear();
                HomeMenu();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Thank you for banking with us...");
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void ReturnToMenu()
        {
            Console.WriteLine("\n\n");
            var menuItem = AnsiConsole.Prompt(new SelectionPrompt<string>()
          .PageSize(10)
          .MoreChoicesText("[grey](Move up and down to reveal more items)[/]")
          .AddChoices("MainMenu")
          .AddChoices("LogOut"));

            if (menuItem.Contains("MainMenu"))
            {
                Console.Clear();
                MainMenu();
            }
            else if (menuItem.Contains("LogOut"))
            {
                Console.Clear();
                HomeMenu();
            }
        }
    }
}
