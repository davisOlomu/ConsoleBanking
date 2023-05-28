using System;
using System.Data;
using System.Threading;
using System.Globalization;
using ConsoleBankDataAccess;
using static ConsoleBanking.Login;


namespace ConsoleBanking
{
    public class Transactions
    {
        // Sign Up
        public static void OpenAccount()
        {
            AccountInfo newUserAccount= new AccountInfo();
            AccountInfo.CreateNewAccount();
        }

        // Retrieve balance
        public static void CheckAccountBalance()
        {
            Designs.CenterTextNewLine("\n\n\n\n");
        
            if (dbAccess.ReadFromCustomerWithUsername(user))
            {
                Notifications.WaitWindow();
                Console.WriteLine($"The balances on this account as at {DateTime.Now} are as follows.\n");

                Console.WriteLine($"Current Balance\t\t:{user.Balance.ToString("C", CultureInfo.CurrentUICulture)}");
                Console.WriteLine($"Available Balance\t:{user.Balance.ToString("C", CultureInfo.CurrentUICulture)}\n\n");

                Designs.DrawLine();
                Console.BackgroundColor = ConsoleColor.Black;

                Menu.ReturnToMenu();
            }
        }

        // Withdraw
        public static void MakeWithdrawal()
        {
            Designs.CenterTextNewLine("\n\n\n\n");
  
            // Read From Db
            if (dbAccess.ReadFromCustomerWithUsername(user))
            {
                Notifications.WaitWindow();

                Console.Write("Description: ");
                string description = Console.ReadLine();

                Console.Write("Amount:  $");
              
                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    var withdraw = new TransactionModel { TransactionDescription = description, TransactionAmount = amount };

                    if (amount > user.Balance)
                    {
                        Notifications.WaitWindow();
                        Designs.CenterTextNewLine("Insufficient funds!\n");

                        Thread.Sleep(2000);
                        Console.Clear();

                        Notifications.TransactionFailed();

                        withdraw.TransactionStatus = TransactionStatus.Unsucessfull;
                        withdraw.TransactionType = TransactionType.Debit;

                    }
                    else if (amount <= 0)
                    {
                        Notifications.WaitWindow();
                        Designs.CenterTextNewLine("Withdraw amount must be positive.\n");

                        Thread.Sleep(2000);
                        Console.Clear();

                        Notifications.TransactionFailed();

                        withdraw.TransactionStatus = TransactionStatus.Unsucessfull;
                        withdraw.TransactionType = TransactionType.Debit;
                    }
                    else
                    {
                        Notifications.WaitWindow();
                        Notifications.TransactionSucess();

                        withdraw.TransactionStatus = TransactionStatus.Sucessfull;
                        withdraw.TransactionType = TransactionType.Debit;

                        user.Balance -= amount;
                        dbAccess.UpdateBalance(user, user.Balance);
                    }

                    // Log this transaction in Db
                    dbAccess.CreateTransaction(withdraw, user.UserName);

                    Console.WriteLine(TransactionReceipt.Receipt(withdraw) + "\n\n");

                    Designs.DrawLine();
                    Console.BackgroundColor = ConsoleColor.Black;

                    Menu.ReturnToMenu();
                }
                else
                {
                    Console.Clear();
                    Designs.CenterTextNewLine("Wrong Input!");

                    Designs.CenterTextNewLine("Enter a valid Amount");
                    Thread.Sleep(1500);

                    MakeWithdrawal();
                    Console.Clear();
                }
            }
        }

        // Deposit
        public static void MakeDeposit()
        {
            Designs.CenterTextNewLine("\n\n\n\n");

            // Read From Db
            if (dbAccess.ReadFromCustomerWithUsername(user))
            {
                Notifications.WaitWindow();

                Console.Write("Description: ");
                string description = Console.ReadLine();

                Console.Write("Amount:  $");

                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    var deposit = new TransactionModel { TransactionDescription = description, TransactionAmount = amount };

                    if (amount <= 0)
                    {
                        Console.Clear();

                        Notifications.WaitWindow();
                        Designs.CenterTextNewLine("Amount of deposit must be positive.\n");

                        Thread.Sleep(2000);
                        Console.Clear();

                        Notifications.TransactionFailed();

                        deposit.TransactionStatus = TransactionStatus.Unsucessfull;
                        deposit.TransactionType = TransactionType.Credit;
                    }
                    else
                    {
                        Notifications.WaitWindow();
                        Notifications.TransactionSucess();

                        deposit.TransactionStatus = TransactionStatus.Sucessfull;
                        deposit.TransactionType = TransactionType.Credit;

                        user.Balance += amount;
                        dbAccess.UpdateBalance(user, user.Balance);
                    }

                    // Log this transaction in Db
                    dbAccess.CreateTransaction(deposit, user.UserName);

                    Console.WriteLine(TransactionReceipt.Receipt(deposit) + "\n\n");

                    Designs.DrawLine();
                    Console.BackgroundColor = ConsoleColor.Black;

                    Menu.ReturnToMenu();
                }
                else
                {
                    Console.Clear();
                    Designs.CenterTextNewLine("Wrong Input!");

                    Designs.CenterTextNewLine("Enter a valid Amount");
                    Thread.Sleep(1500);

                    MakeDeposit();
                    Console.Clear();
                }
            }
        }

        // Get Account info.
        public static void ViewAccountDetails()
        {
            Designs.CenterTextNewLine("\n\n\n\n");           

            if (dbAccess.ReadFromCustomerWithUsername(user))
            {
                Notifications.WaitWindow();

                Console.WriteLine($"Account Name: {user.FirstName} {user.LastName}\nAccount Number: {user.AccountNumber}\nAccount Type: {user.AccountType}\nEmail: {user.Email}\nAccount Balance: {user.Balance.ToString("C", CultureInfo.CurrentUICulture)}\nDate Opened:{user.DateCreated.ToShortDateString()}\nTime Opened: {user.TimeCreated}\n\n");
            }

            Designs.DrawLine();
            Console.BackgroundColor = ConsoleColor.Black;

            Menu.ReturnToMenu();
        }


        // Get all transactions as table.
        public static void ViewTransactionHistory()
        {
            Designs.CenterTextNewLine("\n\n\n\n");
           
            Notifications.WaitWindow();

            DataTable transactionTable = dbAccess.ReadFromTransactionAsTable(user.UserName);

            // Reset console size to have a better view of transactions
            var width = Console.LargestWindowWidth;
            var heigt = Console.LargestWindowHeight;

            Console.SetWindowSize(width, heigt);

            // Display data
            DisplayTransactionsTable(transactionTable);

            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("\n\n");

            Menu.ReturnToMenu();

            // Iterate over table data.
            static void DisplayTransactionsTable(DataTable table)
            {            
                for (int curCol = 0; curCol < table.Columns.Count; curCol++)
                {                 
                    Console.Write($"{table.Columns[curCol].ColumnName}\t\t");
                }

                Console.WriteLine("\n");
                for (int curRow = 0; curRow < table.Rows.Count; curRow++)
                {
                    for (int curCol = 0; curCol < table.Columns.Count; curCol++)
                    {
                        Console.Write($"{table.Rows[curRow][curCol]}\t");                      
                    }
                    Console.WriteLine("\n");
                }
            }
        }
    }
}

