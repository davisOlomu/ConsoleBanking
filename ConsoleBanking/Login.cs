using ConsoleBankDataAccess;
using System;
using System.Threading;

namespace ConsoleBanking
{
    public class Login
    {
        /// <summary>
        /// Wrong code approach here
        /// Exposing static data. 
        /// </summary>
        public static AccountModel user = new AccountModel();
        private static readonly DataLayer databaseAccess = new DataLayer();

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
            user.UserName = Console.ReadLine();
            string sql = $"Select * From Customer Where Username = '{user.UserName}'";

            while (true)
            {
                if (string.IsNullOrEmpty(user.UserName))
                {
                    Console.Clear();
                    Designs.CenterTextNewLine("Username cannot be empty..");
                    Thread.Sleep(2000);
                    Console.Clear();
                    Designs.CenterTextSameLine("Username: ");
                    user.UserName = Console.ReadLine();
                }
                else if (!(databaseAccess.GetUser(user, sql)))
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
            Designs.CenterTextSameLine("Password: ");
            string password = Console.ReadLine();
            Thread.Sleep(2000);
            Console.Clear();

            while (!(user.UserName != null && password == user.Password))
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