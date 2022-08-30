using System;
using System.Text;
using System.Threading;
using System.Globalization;

namespace ConsoleBanking
{

    /// <summary>
    /// Class <c>Transactions</c> captures all transactions available to the user.
    /// </summary>
    /// <param name="TransactionStatus"> Notify user if transaction is Sucessfull or Unsucessfull</param>
    internal class Transactions : Account
    {

        public decimal TransactionAmount { get; }
        public string TransactionDescription { get; }
        public DateTime TransactionDate { get; }
        public DateTime TransactionTime { get; }
        public string TransactionStatus { get; set; }


        public Transactions() { }
        public Transactions(string description, decimal amount)
        {
            TransactionAmount = amount;
            TransactionDescription = description;
            TransactionDate = DateTime.Now.ToLocalTime();
            TransactionTime = DateTime.Now.ToLocalTime();
        }

        public void OpenAnAccount()
        {
            AccountCreationForm();
        }

        public void CheckAccountBalance()
        {
            AccountBalance();

            Console.WriteLine("\n\n");
            Dashboard.ReturnToMenu();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        /// <param name="amount"></param>
        public static void MakeWithdrawal(string description, decimal amount)
        {
            var withdraw = new Transactions(description, -amount);

            if (amount > Balance)
            {
                WaitWindow();
                Console.WriteLine("\t\t\t\t\tInsufficient funds!\n");

                Thread.Sleep(2000);
                Console.Clear();

                TransactionFailed();

                allTransactions.Add(withdraw);
                withdraw.TransactionStatus = "Unsucessful";

            }

            else if (amount <= 0)
            {
                WaitWindow();
                Console.WriteLine("\t\t\t\t\tWithdraw amount must be positive.\n");

                Thread.Sleep(2000);
                Console.Clear();

                TransactionFailed();

                allTransactions.Add(withdraw);
                withdraw.TransactionStatus = "Unsucessful";
            }

            else
            {
                WaitWindow();
                TransactionSucess();

                allTransactions.Add(withdraw);
                withdraw.TransactionStatus = "Sucessful";
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        /// <param name="amount"></param>
        public static void MakeDeposit(string description, decimal amount)
        {
            var deposit = new Transactions(description, amount);

            if (amount <= 0)
            {
                Console.Clear();

                WaitWindow();
                Console.WriteLine("\t\t\t\t\tAmount of deposit must be positive.\n");

                Thread.Sleep(2000);
                Console.Clear();

                TransactionFailed();

                allTransactions.Add(deposit);
                deposit.TransactionStatus = "Unsucessful";

            }

            else
            {
                WaitWindow();
                TransactionSucess();

                allTransactions.Add(deposit);
                deposit.TransactionStatus = "Sucessful";
            }
        }


        public static void ViewAccountDetails()
        {
            WaitWindow();

            Console.WriteLine($"Account Name: {_newAccountCreated.AccountOwner}\nAccount Number: {_newAccountCreated.AccountNumber}\nAccount Type: {ChooseType}\nPhone Number: +234{_newAccountCreated.Contact.ToString("(###) ### ####", CultureInfo.CurrentUICulture)}\nAddress: {_newAccountCreated.Address}\nAccount Balance: {Balance.ToString("C", CultureInfo.CurrentUICulture)}\nDate Opened:{_newAccountCreated.DateCreated.ToShortDateString()}\nTime Opened: {_newAccountCreated.TimeCreated.ToShortTimeString()}\n\n");

            Dashboard.ReturnToMenu();
        }


        public static string ViewAllTransactions()
        {
            WaitWindow();

            StringBuilder accTransactions = new StringBuilder();

            accTransactions.AppendLine("Date\t\tTime\t\tAmount\t\tStatus\t\t\tDescription\n");

            foreach (var item in allTransactions)
            {
                accTransactions.AppendLine($"{item.TransactionDate.ToShortDateString()}\t{item.TransactionTime.ToShortTimeString()}\t{item.TransactionAmount.ToString("C", CultureInfo.CurrentUICulture)}\t{item.TransactionStatus}\t\t{item.TransactionDescription}");
            }

            return accTransactions.ToString();
        }


        public static void TransactionSucess()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t\tTransaction Sucessful!");

            Thread.Sleep(1000);
            Console.Clear();
        }


        public static void TransactionFailed()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t\tTransaction Unsucessful!");

            Thread.Sleep(1000);
            Console.Clear();
        }


        public static void WaitWindow()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t\tPlease Wait");

            Thread.Sleep(1500);
            Console.Clear();
        }

        /// <summary>
        /// A nice receipt like  details of each transaction. In the future this can be printed
        /// </summary>
        /// <returns>Transaction Receipt</returns>
        public static string TransactionReceipt()
        {
            StringBuilder transReceipt = new StringBuilder();

            Transactions[] myTrans = allTransactions.ToArray();

            for (int i = 1; i < 2; i++)
            {
                transReceipt.AppendLine("The details of this transaction are shown below:\nTransaction Notification\n");

                transReceipt.AppendLine($"Account Number: {_newAccountCreated.AccountNumber}\nDescription: {myTrans[^1].TransactionDescription}\nAmount: {myTrans[^1].TransactionAmount.ToString("C", CultureInfo.CurrentUICulture)}\nValue Date: {myTrans[^1].TransactionDate.ToShortDateString()}\nTime: {myTrans[^1].TransactionTime.ToShortTimeString()}\nStatus: {myTrans[^1].TransactionStatus}\n");

                transReceipt.AppendLine($"The balances on this account as at {myTrans[^1].TransactionDate.ToShortDateString()} are as follows;");

                transReceipt.AppendLine($"Current Balance     :{Balance.ToString("C", CultureInfo.CurrentUICulture)}");
                transReceipt.AppendLine($"Available Balance   :{Balance.ToString("C", CultureInfo.CurrentUICulture)}");

            }

            return transReceipt.ToString();
        }
    }
}

