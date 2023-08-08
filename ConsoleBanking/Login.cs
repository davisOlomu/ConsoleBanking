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
            Console.ForegroundColor = ConsoleColor.White;
            Designs.CenterTextNewLine("\n\n");
            Designs.CenterTextSameLine("Username: ");
            UserLoggedIn.UserName = Console.ReadLine();
            string sql = $"Select * From Customer Where Username = '{UserLoggedIn.UserName}'";

            while (true)
            {
                if (string.IsNullOrEmpty(UserLoggedIn.UserName))
                {
                    Console.Clear();
                    Designs.CenterTextNewLine("Username cannot be empty..");
                    Thread.Sleep(2000);
                    Console.Clear();
                    Designs.CenterTextSameLine("Username: ");
                    UserLoggedIn.UserName = Console.ReadLine();
                }
                else if (!(databaseAccess.GetUser(UserLoggedIn, sql)))
                {
                    Console.Clear();
                    Designs.CenterTextNewLine("Username not found! ");
                    Thread.Sleep(2000);
                    Console.Clear();
                    VerifyUser();
                }
                else
                {
                    break;
                }
            }
     
            var password = AnsiConsole.Prompt(
              new TextPrompt<string>("[green]Password:[/]")
             .PromptStyle("red")
             .Secret());
            Thread.Sleep(2000);
            Console.Clear();

            while (!(UserLoggedIn.UserName != null && password == UserLoggedIn.Password))
            {
                Console.Clear();
                Designs.CenterTextNewLine("Incorrect password!");
                Thread.Sleep(2000);
                Console.Clear();
                Designs.CenterTextSameLine("Password: ");
                password = Console.ReadLine();
                Console.Clear();
            }
            Menu.MainMenu();
        }
    }
}