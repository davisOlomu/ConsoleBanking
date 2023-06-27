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
            StringBuilder transReceipt = new StringBuilder();

            if (dbAccess.ReadFromCustomerWithUsername(user))
            {
                transReceipt.AppendLine("The details of this transaction are shown below:\n\nTransaction Notification\n");
                transReceipt.AppendLine($"Account Number: {user.AccountNumber}\nDescription: {model.TransactionDescription}\nAmount: {model.TransactionAmount.ToString("C", CultureInfo.CurrentUICulture)}\nValue Date: {DateTime.Now.ToShortDateString()}\nTime: {DateTime.Now.ToShortTimeString()}\nStatus: {model.TransactionStatus}\nTransaction Type: {model.TransactionType}\n\n");
                transReceipt.AppendLine($"The balances on this account as at {DateTime.Now} are as follows;\n");
                transReceipt.AppendLine($"Current Balance     :{user.Balance.ToString("C", CultureInfo.CurrentUICulture)}");
                transReceipt.AppendLine($"Available Balance   :{user.Balance.ToString("C", CultureInfo.CurrentUICulture)}");
            }
            return transReceipt.ToString();
        }
    }
}
