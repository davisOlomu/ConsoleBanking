using System;
using System.Threading;
using Spectre.Console;

namespace ConsoleBanking
{
    /// <summary>
    /// Status about transactions.
    /// </summary>
    public class TransactionNotifications
    {
        public static void ReturnSuccessfull()
        {
            Console.Clear();
            AnsiConsole.Write(new Markup("[red]Transaction Successfull![/]").Centered());
            Thread.Sleep(2000);
            Console.Clear();
        }
        public static void ReturnUnsuccessfull()
        {
            Console.Clear();
            AnsiConsole.Write(new Markup("[red]Transaction Unsuccessfull![/]").Centered());
            Thread.Sleep(2000);
            Console.Clear();
        }
        public static void InProgress()
        {
            Console.Clear();
            AnsiConsole.Write(new Markup("[red]Please Wait..[/]").Centered());
            Thread.Sleep(1500);
            Console.Clear();
        }
    }
}
