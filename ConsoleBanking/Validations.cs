using System;
using System.Threading;
using ConsoleBankDataAccess;
using static ConsoleBanking.Login;

namespace ConsoleBanking
{
    internal static class Validations
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly DataLayer dbAccess = new DataLayer();

        /// <summary>
        ///   Verify that pin is within the range of 9999 and 999
        /// </summary>
        /// <returns>pin</returns>
        public static int ValidatePin()
        {
            if (int.TryParse(Console.ReadLine(), out int pin))
            {
                if (pin < 9999 && pin > 999)
                {
                    return pin;
                }
                else
                {
                    Console.Clear();
                    Designs.CenterTextNewLine("Invalid Pin!");
                    Designs.CenterTextNewLine(" Pin must be below 9999 and above 999");
                    Designs.CenterTextNewLine("Re-Enter Pin.\n");
                    Console.Write("Pin: ");
                    pin = ValidatePin();
                }
            }
            else
            {
                Console.Clear();
                Designs.CenterTextNewLine("Invalid pin format");
                Designs.CenterTextNewLine("Re-Enter Pin");
                Console.Write("Pin: ");
                pin = ValidatePin();
            }
            return pin;
        }

        /// <summary>
        /// Verify that Username  doesn't already exist in the database.
        /// </summary>
        /// <returns>username if it doesnt't already exist</returns>
        public static string ValidateUsername()
        {
            string username = Console.ReadLine();;

            while (dbAccess.VerifyUserName(username))
            {
                Designs.CenterTextNewLine("Username already exist.");
                Designs.CenterTextNewLine("Re-Enter Username");
                username = Console.ReadLine();
                dbAccess.VerifyUserName(username);
            }
            return username;
        }

     /// <summary>
     /// allows users choose from a list of constants
     /// </summary>
     /// <param name="type">Account types</param>
     /// <param name="option">User's choice</param>
     /// <returns>account type selected</returns>
        public static AccountType ValidateAccoutType()
        {
            AccountType type = new Int32();

            void GetAccountType()
            {
                Console.WriteLine("Select Account type:\n1.Savings\n2.Current\n3.Checking");
                ConsoleKeyInfo option = Console.ReadKey();
                Console.WriteLine();

                switch (option.Key)
                {
                    case ConsoleKey.NumPad1:
                        type = AccountType.Savings;
                        break;
                    case ConsoleKey.NumPad2:
                        type = AccountType.Current;
                        break;
                    case ConsoleKey.NumPad3:
                        type = AccountType.Checking;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Wrong Input!");
                        Thread.Sleep(2000);
                        Console.Clear();
                        GetAccountType();
                        break;
                }
            }
            GetAccountType();
            return type;
        }

        /// <summary>
        /// Verify that input is a valid amount using the deposit 
        /// </summary>
        /// <returns>amount if it is a valid input</returns>
        public static decimal ValidateInitialDeposit()
        {
            decimal amount = new decimal();

            void deposit()
            {
                Console.Write("\nOpening amount: $");

                if (!decimal.TryParse(Console.ReadLine(), out amount))
                {
                    Console.Clear();
                    Designs.CenterTextNewLine("Wrong Input! ");
                    Designs.CenterTextNewLine("Re-Enter Amount ");
                    Thread.Sleep(2000);
                    Console.Clear();
                    deposit();
                }
            }
            deposit();
            return amount;
        }
    }
}
