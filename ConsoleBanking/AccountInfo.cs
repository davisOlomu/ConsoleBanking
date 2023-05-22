using System;
using System.Threading;
using ConsoleBankDataAccess;
using static ConsoleBanking.Validations;

namespace ConsoleBanking
{
    public class AccountInfo
    {  
        public static string firstname;
        public static string lastname;
        public static string email;
        public static string username;
        public static string password;
        public static int pin;
        public static decimal initialDeposit;
        public static AccountType accountType;

        // SignUp!(new app user)
        public static void CreateNewAccount()
        {
            Console.WriteLine("Fill in the following details.");
            Console.WriteLine("Press Enter Key after each field.\n\n");

            Console.Write("Firstname: ");
            firstname = Console.ReadLine();

            Console.Write("Lastname: ");
            lastname = Console.ReadLine();

            Console.Write("Email: ");
            email = Console.ReadLine();

            Console.Write("UserName: ");
            username = ValidateUsername();      

            Console.Write("Password: ");
            password = Console.ReadLine();

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
            Console.WriteLine("\n");             

            accountType = ValidateAccoutType();
            initialDeposit = ValidateInitialDeposit();

            Account.CreateAccount();
        }
    }
}

