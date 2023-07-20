using System;
using System.Threading;
using ConsoleBankDataAccess;
using System.Data.SqlClient;

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

            try
            {
                if (databaseAccess.GetUser(user, sql))
                {
                    Designs.CenterTextSameLine("Password: ");
                    string password = Console.ReadLine();
                    Thread.Sleep(3000);
                    Console.Clear();

                    if (user.UserName != null && password == user.Password)
                    {
                        Menu.MainMenu();
                    }
                    else
                    {
                        Console.Clear();
                        Designs.CenterTextNewLine("Incorrect password!");
                        Thread.Sleep(3000);
                        Console.Clear();
                        VerifyUser();
                    }
                }
                else
                {
                    Console.Clear();
                    Designs.CenterTextNewLine("Username not found! ");
                    Thread.Sleep(3000);
                    Console.Clear();
                    VerifyUser();
                }
            }
            catch (SqlException e)
            {
                Console.Clear();
                Designs.CenterTextNewLine(e.Message);
                Thread.Sleep(3000);
                Console.Clear();
                VerifyUser();
            }
        }
    }
}
