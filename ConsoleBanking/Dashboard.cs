using System;
using System.Threading;

namespace ConsoleBanking
{

    /// <summary>   
    /// Entry point of application
    /// </summary>
    internal class Dashboard
    {
        static void Main()
        {
             
            Console.Title = "Console Banking";
            Console.SetWindowSize(100, 30);

            Console.WriteLine("\t\t\t\t\t\tWelcome...\n");
            Console.WriteLine("\t\t\t\tFirst time user's must open an account.\n\n");

            Console.WriteLine("1> Proceed");
            Console.WriteLine("2> Exit Application");

            ConsoleKeyInfo userOption = Console.ReadKey();
            Transactions.WaitWindow();

            Console.Clear();


            switch (userOption.Key)
            {
                case ConsoleKey.NumPad1:
                    Console.Clear();

                    Account newCustomer = new Account();
                    newCustomer.AccountCreationForm();
                    break;

                case ConsoleKey.NumPad2:
                    Console.Clear();
                    Console.WriteLine("Thank you for Banking with us.");

                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Wrong Input!");
                    Thread.Sleep(1500);

                    Console.Clear();
                    Dashboard.Main();
                    break;
            }
        }


        public static void MainMenu()
        {
            Transactions myTransactions = new Transactions();

            Console.WriteLine("1> Make Withdrawal.");
            Console.WriteLine("2> Make Deposit.\n");
            Console.WriteLine("3> Check Balance.");
            Console.WriteLine("4> Check account Information.\n");
            Console.WriteLine("5> Check all Transactions.");
            Console.WriteLine("6> Exit Application.");


            ConsoleKeyInfo userOption = Console.ReadKey();
            Console.Clear();


            switch (userOption.Key)
            {

                case ConsoleKey.NumPad1:
                    myTransactions.Withdraw();
                    break;

                case ConsoleKey.NumPad2:
                    myTransactions.Deposit();
                    break;

                case ConsoleKey.NumPad3:
                    myTransactions.CheckAccountBalance();
                    break;

                case ConsoleKey.NumPad4:
                    Transactions.ViewAccountDetails();
                    break;

                case ConsoleKey.NumPad5:
                    Console.WriteLine(Transactions.ViewAllTransactions());
                    Console.WriteLine("\n\n");
                    ReturnToMenu();
                    break;


                case ConsoleKey.NumPad6:
                    Console.Clear();
                    Console.WriteLine("Thank you for Banking with us.");
                    Environment.Exit(0);
                    break;


                default:
                    Console.WriteLine("Wrong Input! \n");
                    ReturnToMenu();
                    break;
            }
        }


        public static void ReturnToMenu()
        {
            Console.WriteLine("0> Go to Main Menu.");
            Console.WriteLine("1> Exit Application. ");

            ConsoleKeyInfo userOption = Console.ReadKey();

            if (userOption.Key == ConsoleKey.NumPad0)
            {
                Console.Clear();
                Dashboard.MainMenu();
            }


            else if (userOption.Key == ConsoleKey.NumPad1)
            {
                Console.Clear();
                Console.WriteLine("Thank you for Banking with us.");
                Environment.Exit(0);
            }

            else
            {
                Console.Clear();
                Console.WriteLine("Wrong Input!");
                Environment.Exit(0);
            }
        }
    }
}
    

