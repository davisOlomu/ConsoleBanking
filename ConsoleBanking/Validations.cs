using System;
using System.Threading;
using ConsoleBankDataAccess;
using static ConsoleBanking.Login;


namespace ConsoleBanking
{
    public class Validations
    {
        // Validate Pin
        public static int ValidatePin()
        {
            if (!int.TryParse(Console.ReadLine(), out int pin))
            {
                //Designs.CenterNewLine("Invalid pin format");
                //Designs.CenterNewLine("Re-Enter Pin");

                //ValidatePin();
                throw new InvalidPinException("Invalid pin format");
            }
            else
            {
                // Check if Pin already exist
                while (dbAccess.VerifyPin(pin))
                {
                    Designs.CenterNewLine("Pin already exist.");
                    Designs.CenterNewLine("Re-Enter Pin");

                    pin = int.Parse(Console.ReadLine());
                    dbAccess.VerifyPin(pin);
                }
            }
            return pin;
        }


        // Validate Username
        public static string ValidateUsername()
        {
            string username = Console.ReadLine();

            // Check if username already exist
            while (dbAccess.VerifyUserName(username))
            {
                Designs.CenterNewLine("Username already exist.");
                Designs.CenterNewLine("Re-Enter Username");

                username = Console.ReadLine();

                dbAccess.VerifyUserName(username);
            }
            return username;
        }


        // Validate Account Type
        public static AccountType ValidateAccoutType()
        {
            // Verify account type input
            AccountType accountType = new Int32();

            void SelectAccountType()
            {
                Console.WriteLine("Select Account type:\n1.Savings\n2.Current\n3.Checking");

                ConsoleKeyInfo selectAccountType = Console.ReadKey();
                Console.WriteLine();

                switch (selectAccountType.Key)
                {
                    case ConsoleKey.NumPad1:
                        accountType = AccountType.Savings;
                        break;

                    case ConsoleKey.NumPad2:
                        accountType = AccountType.Current;
                        break;

                    case ConsoleKey.NumPad3:
                        accountType = AccountType.Checking;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Wrong Input!");
                        Thread.Sleep(2000);

                        Console.Clear();
                        SelectAccountType();
                        break;
                }
            }
            SelectAccountType();
            return accountType;
        }

        // Validate Initial deposit
        public static decimal ValidateInitialDeposit()
        {
            Console.Write("\nOpening amount: $");

            if (!decimal.TryParse(Console.ReadLine(), out decimal initialDeposit))
            {
                Designs.CenterNewLine("Wrong Input! ");
                Designs.CenterNewLine("Re-Enter Amount ");

                Thread.Sleep(2000);
                Console.Clear();

                ValidateInitialDeposit();
            }
            return initialDeposit;
        }
    }
}
