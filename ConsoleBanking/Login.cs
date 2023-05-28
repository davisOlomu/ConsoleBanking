using System;
using System.Threading;
using ConsoleBankDataAccess;
using System.Data.SqlClient;

namespace ConsoleBanking
{
    // Sign in (Existing users)
    public class Login
    {
        public static AccountModel user = new AccountModel();
        public static DataAccess dbAccess = new DataAccess();

        public static void LogInMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;

            Designs.CenterTextNewLine("\n\n");
            Designs.CenterTextSameLine("Username: ");

            user.UserName = Console.ReadLine();

            try
            {
                if (dbAccess.ReadFromCustomerWithUsername(user))
                {
                    Designs.CenterTextSameLine("Password: ");
                    string password = Console.ReadLine();

                    Thread.Sleep(6000);
                    Console.Clear();

                    // Match username to password
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

                        LogInMenu();
                    }
                }
                else
                {
                    Console.Clear();
                    Designs.CenterTextNewLine("Username not found! ");

                    Thread.Sleep(5000);
                    Console.Clear();

                    LogInMenu();
                }
            }
            catch (SqlException e)
            {
                Console.Clear();
                Designs.CenterTextNewLine(e.Message);

                Thread.Sleep(5000);
                Console.Clear();

                LogInMenu();
            }
        }
    }
}
