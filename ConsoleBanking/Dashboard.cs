using System;

namespace ConsoleBanking
{
    internal class Dashboard
    {
        /// <summary>
        /// Login or create a new account.
        /// </summary>
        static void Main()
        {
            Console.Title = "Console Banking System[v1.0.0.0]";
            Console.SetWindowSize(100, 25);
            Console.ForegroundColor = ConsoleColor.White;
            Menu.HomeMenu();
        }
    }
}


