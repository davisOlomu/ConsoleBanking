using System;
using System.Security.Cryptography;
using System.Text;


namespace ConsoleBanking
{
    public class SecurePassword
    {
        //Method takes in string and encrypts it before it's passed to the DB
        public static string Protect()
        {
           string input = Console.ReadLine();
            SHA1 encrypt = SHA1.Create();

           // byte[] key = encrypt.ComputeHash(Encoding.Default.GetBytes(input));
           byte[] key = encrypt.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder protectedString = new StringBuilder();

            for (int i = 0; i < key.Length; i++)
            {
                protectedString.Append(key[i].ToString());
            }      
            return protectedString.ToString();
        }
    }
}
