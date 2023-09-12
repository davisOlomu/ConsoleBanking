using ConsoleBankDataAccess;
using System;
using System.Threading;
using Spectre.Console;

namespace ConsoleBanking
{
    /// <summary>
    /// 
    /// </summary>
    public class Login
    {
        private static AccountModel userLoggedIn = new AccountModel();
        private static readonly DataLayer databaseAccess = new DataLayer();

        /// <summary>
        /// Expose user currently logged in.
        /// </summary>
        public static AccountModel UserLoggedIn
        {
            get { return userLoggedIn; }
            set { userLoggedIn = value; }
        }

        /// <summary>
        /// Validate an existing user,
        /// using user's username and pin.
        /// </summary>
        /// <param name="user">sucessfully validated user</param>
        public static void VerifyUser()
        {
         
            AnsiConsole.Write(new Markup("[blue]Username: [/]").Centered());
            Console.SetCursorPosition(55, 9);
            Console.ForegroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
            UserLoggedIn.UserName = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            string sql = $"Select * From Customer Where Username = '{UserLoggedIn.UserName}'";

            while (true)
            {
                if (string.IsNullOrEmpty(UserLoggedIn.UserName))
                {
                    Console.Clear();
                    AnsiConsole.Write(new Markup("[red]Username cannot be empty..[/]").Centered());
                    Thread.Sleep(2000);
                    Console.Clear();
                    AnsiConsole.Write(new Markup("[blue]Username: [/]").Centered());
                    Console.ForegroundColor = ConsoleColor.Red;
                    UserLoggedIn.UserName = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (!(databaseAccess.GetUser(UserLoggedIn, sql)))
                {
                    Console.Clear();
                    AnsiConsole.Write(new Markup("[red]Username not found! [/]").Centered());
                    Thread.Sleep(2000);
                    Console.Clear();
                    VerifyUser();
                }
                else
                {
                    break;
                }
            }

            string passwordLiteral = "Password: ";
            Console.SetCursorPosition((Console.WindowWidth - passwordLiteral.Length) / 2, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.Blue;
            var password = AnsiConsole.Prompt(
              new TextPrompt<string>(passwordLiteral)
             .PromptStyle("red")
             .Secret());
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            while (!(UserLoggedIn.UserName != null && password == UserLoggedIn.Password))
            {
                Console.Clear();
                AnsiConsole.Write(new Markup("[red]Incorrect password![/]").Centered());
                Thread.Sleep(2000);
                Console.Clear();
                AnsiConsole.Write(new Markup("[blue]Password: [/]").Centered());
                Console.ForegroundColor = ConsoleColor.Red;
                password = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
            }
            Menu.MainMenu();
        }
    }
}