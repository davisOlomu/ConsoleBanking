using System;
using System.Threading;
using ConsoleBankDataAccess;
using static ConsoleBanking.Validations;

namespace ConsoleBanking
{
    internal static class AccountInformation
    {
        /// <summary>
        /// Collects data, validates them and passes 
        /// accurate data, for account creation.
        /// </summary>
        public static void GetUserDetails()
        {
            Designs.CenterTextNewLine("Fill in the following details.");
            Designs.CenterTextNewLine("Press Enter Key after each field.\n\n");
            Thread.Sleep(3000);
            Console.Clear();

            Console.WriteLine("Personal details: \n\n");
            Console.Write("Firstname: ");
            string firstName = Console.ReadLine();

            Console.Write("Lastname: ");
            string lastName = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Security: \n\n");
            Console.Write("UserName: ");
            string username = ValidateUsername();

            Console.Write("Password: ");
            //password = SecurePassword.Protect();
            string password = Console.ReadLine();

            int pin = 0;
            bool isnotPin = true;

            while (isnotPin)
            {
                try
                {
                    Console.Write("Pin: ");
                    pin = ValidatePin();
                    isnotPin = false;
                }
                catch (InvalidPinException e)
                {
                    Console.WriteLine(e.Message);
                    Thread.Sleep(3000);
                    Console.Clear();
                }
            }
            Console.Clear();
            Console.WriteLine("Account details: \n\n ");
            AccountType type = ValidateAccoutType();
            decimal initialDeposit = ValidateInitialDeposit();

            Account.CreateAccount(firstName, lastName, email, username, password, initialDeposit, type, pin);
        }
    }
}

