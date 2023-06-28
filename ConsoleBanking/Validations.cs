using System;
using System.Threading;
using ConsoleBankDataAccess;
using static ConsoleBanking.Login;

namespace ConsoleBanking
{
    public class Validations
    {    
        // Verify Pin
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
        // Verify Username
        public static string ValidateUsername()
        {
            string username = Console.ReadLine();;

            // Check if username already exist in Db
            while (dbAccess.VerifyUserName(username))
            {
                Designs.CenterTextNewLine("Username already exist.");
                Designs.CenterTextNewLine("Re-Enter Username");
                username = Console.ReadLine();
                dbAccess.VerifyUserName(username);
            }
            return username;
        }
        // Validate Account Type
        public static AccountType ValidateAccoutType()
        {
            // Verify account type input
            AccountType type = new Int32();

            void GetAccountType()
            {
                Console.WriteLine("Select Account type:\n1.Savings\n2.Current\n3.Checking");
                ConsoleKeyInfo accountTypeOption = Console.ReadKey();
                Console.WriteLine();

                switch (accountTypeOption.Key)
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
        // Verify Initial deposit
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
