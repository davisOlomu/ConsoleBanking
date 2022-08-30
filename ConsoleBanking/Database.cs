using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleBanking
{
    internal class Database : Account
    {

        public string FirstName { get; }
        public string LastName { get; }
        public decimal Amount { get; }
        public DateTime DateAdded { get; }
        public DateTime TimeAdded { get; }


        public Database() { }

        public Database(string firstName, string lastName, decimal amount)
        {
            FirstName = firstName;
            LastName = lastName;
            Amount = amount;
            DateAdded = DateTime.Now.ToLocalTime();
            TimeAdded = DateTime.Now.ToLocalTime();
        }


        // Still yet to learn the concepts of using database
        // I'm trying to see if i can create an internal database
        // Using Generic Collections

        // This database is intended to store account details

        // I will manually create an in-memory database


        //public static string ViewAllAccounts()
        //{

        //    StringBuilder accCreated = new StringBuilder();

        //    accCreated.AppendLine("Fullname\tAccountNumber\tOpeningBalance\tDate Created\tTime Created\n");

        //    foreach (KeyValuePair<decimal, Account> account in listOfAcccountsCreated)
        //    {
        //        Account accInfo = account.Value;

        //        accCreated.AppendLine($"{accInfo.AccountOwner.ToUpper()}\t{accInfo.AccountNumber}\t{accInfo.DateCreated.ToShortDateString()}\t{accInfo.TimeCreated.ToShortTimeString()}");
        //    }


        //    Console.WriteLine();

        //    return accCreated.ToString();

        //}


        public void Test()
        {
            List<string> myNames = new List<string>();



        }

    }
}

