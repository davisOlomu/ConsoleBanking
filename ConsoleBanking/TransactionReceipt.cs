using System;
using System.Text;
using System.Globalization;
using ConsoleBankDataAccess;
using static ConsoleBanking.Login;

namespace ConsoleBanking
{
    public class TransactionReceipt
    {
        // View transaction details
        public static string GetReceipt(TransactionModel model)
        {
            StringBuilder receipt = new StringBuilder();

            if (dbAccess.ReadFromCustomerWithUsername(user))
            {
                receipt.AppendLine("The details of this transaction are shown below:\n\nTransaction Notification\n");
                receipt.AppendLine($"Account Number: {user.AccountNumber}\nDescription: {model.TransactionDescription}\nAmount: {model.TransactionAmount.ToString("C", CultureInfo.CurrentUICulture)}\nValue Date: {DateTime.Now.ToShortDateString()}\nTime: {DateTime.Now.ToShortTimeString()}\nStatus: {model.TransactionStatus}\nTransaction Type: {model.TransactionType}\n\n");
                receipt.AppendLine($"The balances on this account as at {DateTime.Now} are as follows;\n");
                receipt.AppendLine($"Current Balance     :{user.Balance.ToString("C", CultureInfo.CurrentUICulture)}");
                receipt.AppendLine($"Available Balance   :{user.Balance.ToString("C", CultureInfo.CurrentUICulture)}");
            }
            return receipt.ToString();
        }
    }
}
