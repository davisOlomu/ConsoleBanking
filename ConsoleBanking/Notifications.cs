﻿using System;
using System.Threading;

namespace ConsoleBanking
{
    /// <summary>
    /// Status about transactions.
    /// </summary>
    public class TransactionNotifications
    {
        public static void Successfull()
        {
            Console.Clear();
            Designs.CenterTextNewLine("Transaction Successfull!");
            Thread.Sleep(2000);
            Console.Clear();
        }
        public static void Unsuccessfull()
        {
            Console.Clear();
            Designs.CenterTextNewLine("Transaction Unsuccessfull!");
            Thread.Sleep(2000);
            Console.Clear();
        }
        public static void InProgress()
        {
            Console.Clear();
            Designs.CenterTextNewLine("Please Wait");
            Thread.Sleep(1500);
            Console.Clear();
        }
    }
}
