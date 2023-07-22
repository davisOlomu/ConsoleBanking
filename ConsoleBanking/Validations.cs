using System;
using System.Threading;
using ConsoleBankDataAccess;

namespace ConsoleBanking
{
    internal static class Validations
    {
        /// <summary>
        /// Database instance.
        /// </summary>
        private static readonly DataLayer databaseAccess = new DataLayer();

        /// <summary>
        /// User must enter a firstname.
        /// </summary>
        /// <returns>Valid firstname</returns>
        public static string ValidateFirstName()
        {
            Console.Write("Firstname: ");
            string firstname = Console.ReadLine();
            if (string.IsNullOrEmpty(firstname))
            {
                Console.Clear();
                Designs.CenterTextNewLine("Wrong Input! ");
                Designs.CenterTextNewLine("Re-Enter Firstname.");
                Thread.Sleep(2000);
                Console.Clear();
                firstname = ValidateFirstName();
            }
            return firstname;
        }

        /// <summary>
        /// User must enter a lastname.
        /// </summary>
        /// <returns>Valid lastname</returns>
        public static string ValidateLastName()
        {
            Console.Write("Lastname: ");
            string lastname = Console.ReadLine();
            if (string.IsNullOrEmpty(lastname))
            {
                Console.Clear();
                Designs.CenterTextNewLine("Wrong Input! ");
                Designs.CenterTextNewLine("Re-Enter Lastname.");
                Thread.Sleep(2000);
                Console.Clear();   
                lastname = ValidateLastName();
            }
            return lastname;
        }

        /// <summary>
        /// Verify that email address is a valid email URL.
        /// </summary>
        /// <returns>A valid email URL</returns>
        public static string ValidateEmail()
        {
            string email = Console.ReadLine();

            return email;
        }

        /// <summary>
        ///   Verify that pin is within the range of 9999 and 999
        /// </summary>
        /// <returns>pin</returns>
        public static int ValidatePin()
        {
            Console.Write("Pin: ");
            if (int.TryParse(Console.ReadLine(), out int pin))
            {
                if (pin <= 9999 && pin > 999)
                {
                    return pin;
                }
                else
                {
                    Console.Clear();
                    Designs.CenterTextNewLine("Invalid Pin!");
                    Designs.CenterTextNewLine(" Pin must be below 9999 and above 999");
                    Designs.CenterTextNewLine("Re-Enter Pin.\n");             
                    pin = ValidatePin();
                }
            }
            else
            {
                Console.Clear();
                Designs.CenterTextNewLine("Invalid pin format");
                Designs.CenterTextNewLine("Re-Enter Pin");
                pin = ValidatePin();
            }
            return pin;
        }

        /// <summary>
        /// Verify that Username doesn't already exist in the database.
        /// </summary>
        /// <returns>username if it doesnt't already exist</returns>
        public static string ValidateUsername()
        {
            Console.Write("UserName: ");
            string username = Console.ReadLine();
            while (databaseAccess.VerifyUserName(username))
            {
                Designs.CenterTextNewLine("Username already exist.");
                Designs.CenterTextNewLine("Re-Enter Username");
                Thread.Sleep(3000);
                Console.Clear();
                username = ValidateUsername();
                databaseAccess.VerifyUserName(username);
            }
            return username;
        }

        /// <summary>
        /// Verify that password contains
        /// at least One uppercase, One lower case,
        /// and One special character.
        /// </summary>
        /// <returns>a password that has at least one uppercase, digit and special character</returns>
        public static string ValidatePassword()
        {
            const string CapitalLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string Digits = "0123456789";
            const string SpecialChars = "~!@#$%^&*()_+=`{}[]\\|':;.,/?<>";
            char[] upperCharArray = CapitalLetters.ToCharArray();
            char[] digitCharArray = Digits.ToCharArray();
            char[] specialCharArray = SpecialChars.ToCharArray();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            if (!(password.IndexOfAny(upperCharArray) == -1))
            {
                if (!(password.IndexOfAny(digitCharArray) == -1))
                {
                    if (!(password.IndexOfAny(specialCharArray) == -1))
                    {
                        return password;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Password must contain at least one special character...");
                        Thread.Sleep(2000);
                        Console.Clear();
                        password = ValidatePassword();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Password must contain at least one digit...");
                    Thread.Sleep(2000);
                    Console.Clear();
                    password = ValidatePassword();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Password must contain at least one uppercase character...");
                Thread.Sleep(2000);
                Console.Clear();
                password = ValidatePassword();
            }
            return password;
        }

        /// <summary>
        /// allows users choose from a list of constant Account types
        /// </summary>
        /// <param name="type">Account types</param>
        /// <param name="option">User's choice</param>
        /// <returns>account type selected</returns>
        public static AccountType ValidateAccoutType()
        {
            AccountType type;
            Console.WriteLine("Select Account type:\n1.Savings\n2.Current\n3.Checking");
            ConsoleKeyInfo userOption = Console.ReadKey();
            Console.WriteLine();

            switch (userOption.Key)
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
                    Designs.CenterTextNewLine("Wrong Input!");
                    Designs.CenterTextNewLine("Re-Select Account Type. ");
                    Thread.Sleep(2000);
                    Console.Clear();
                    Console.Write("Account Type: ");
                    type = ValidateAccoutType();
                    break;
            }
            return type;
        }

        /// <summary>
        /// Verify that inital deposit is at least #1000.
        /// </summary>
        /// <returns>amount if it's at least #1000</returns>
        public static decimal ValidateInitialDeposit()
        {
            Console.Write("\nOpening amount: #");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                if (amount >= 1000)
                {
                    return amount;
                }
                else
                {
                    Console.Clear();
                    Designs.CenterTextNewLine("Initial deposit must be at least #1000.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    amount = ValidateInitialDeposit();
                }
            }
            else
            {
                Console.Clear();
                Designs.CenterTextNewLine("Wrong Input! ");
                Designs.CenterTextNewLine("Re-Enter Amount ");
                Thread.Sleep(2000);
                Console.Clear();        
                amount = ValidateInitialDeposit();
            }
            return amount;
        }
    }
}
