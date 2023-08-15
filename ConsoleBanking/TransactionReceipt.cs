using System;
using System.Text;
using System.Globalization;
using ConsoleBankDataAccess;
using static ConsoleBanking.Login;
using Spectre.Console;

namespace ConsoleBanking
{
    internal static class TransactionReceipt 
    {
        /// <summary>
        /// Database instance.
        /// </summary>
        private static readonly DataLayer databaseAccess = new DataLayer();

        /// <summary>
        /// View Information concerning a particular transaction.
        /// </summary>
        /// <param name="model">models the transaction table</param>
        /// <param name="user">Current user logged in</param>
        /// <returns>differnt information about a transaction</returns>
        public static string GetReceipt(TransactionModel model)
        {        
        StringBuilder receipt = new StringBuilder();
            string sql = $"Select * From Customer Where UserName = '{UserLoggedIn.UserName}'";
            if (databaseAccess.GetUser(UserLoggedIn, sql))
            {
                //var mark = new Markup($"[blue]{receipt.AppendLine("The details of this transaction are shown below:\n\nTransaction Notification\n")}[/]");
                receipt.AppendLine("The details of this transaction are shown below:\n\nTransaction Notification\n");
                receipt.AppendLine($"Account Number: {UserLoggedIn.AccountNumber}\nDescription: {model.TransactionDescription}\nAmount: {model.TransactionAmount.ToString("C", CultureInfo.CurrentUICulture)}\nValue Date: {DateTime.Now.ToShortDateString()}\nTime: {DateTime.Now.ToShortTimeString()}\nStatus: {model.TransactionStatus}\nTransaction Type: {model.TransactionType}\n\n");
                receipt.AppendLine($"The balances on this account as at {DateTime.Now} are as follows;\n");
                receipt.AppendLine($"Current Balance     :{UserLoggedIn.Balance.ToString("C", CultureInfo.CurrentUICulture)}");
                receipt.AppendLine($"Available Balance   :{UserLoggedIn.Balance.ToString("C", CultureInfo.CurrentUICulture)}");
            }
            return receipt.ToString();
        }
    }
}
