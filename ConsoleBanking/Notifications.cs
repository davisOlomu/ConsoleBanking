using System;
using System.Threading;

namespace ConsoleBanking
{
    // Show Transaction notifications
    public class Notifications
    {
        public static void TransactionSucess()
        {
            Console.Clear();
            Designs.CenterTextNewLine("Transaction Sucessful!");
            Thread.Sleep(1000);
            Console.Clear();
        }
        public static void TransactionFailed()
        {
            Console.Clear();
            Designs.CenterTextNewLine("Transaction Unsucessful!");
            Thread.Sleep(1000);
            Console.Clear();
        }
        public static void WaitWindow()
        {
            Console.Clear();
            Designs.CenterTextNewLine("Please Wait");
            Thread.Sleep(1500);
            Console.Clear();
        }
    }
}
