using System;
using System.Threading;
using ConsoleBankDataAccess;
using System.Data.SqlClient;

namespace ConsoleBanking
{
    public class Login
    {
        // Wrong code approach here
        // working on a solution.
        // Exposing static data
        public static AccountModel user = new AccountModel();
        public static readonly DataLayer dbAccess = new DataLayer();

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

            try
            {
                if (dbAccess.GetUser(user, sql))
                {
                    Designs.CenterTextSameLine("Password: ");
                    string password = Console.ReadLine();
                    Thread.Sleep(6000);
                    Console.Clear();

                    if (user.UserName != null && password == user.Password)
                    {
                        Menu.MainMenu();
                    }
                    else
                    {
                        Console.Clear();
                        Designs.CenterTextNewLine("Incorrect password!");
                        Thread.Sleep(5000);
                        Console.Clear();
                        VerifyUser();
                    }
                }
                else
                {
                    Console.Clear();
                    Designs.CenterTextNewLine("Username not found! ");
                    Thread.Sleep(5000);
                    Console.Clear();
                    VerifyUser();
                }
            }
            catch (SqlException e)
            {
                Console.Clear();
                Designs.CenterTextNewLine(e.Message);
                Thread.Sleep(5000);
                Console.Clear();
                VerifyUser();
            }
        }
    }
}
