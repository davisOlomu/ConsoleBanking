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
            Designs.CenterTextNewLine("Fill in the following details.");
            Designs.CenterTextNewLine("Press Enter Key after each field.\n\n");
            Thread.Sleep(3000);
            Console.Clear();

            Console.WriteLine("Personal details: \n\n");
            Console.Write("Firstname: ");
            firstname = Console.ReadLine();

            Console.Write("Lastname: ");
            lastname = Console.ReadLine();

            Console.Write("Email: ");
            email = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Security: \n\n");
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
            Console.Clear();


            Console.WriteLine("Account details: \n\n ");
            accountType = ValidateAccoutType();
            initialDeposit = ValidateInitialDeposit();



            Account.CreateAccount();
        }
    }
}

