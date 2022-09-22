using System.Security.Cryptography;
using System.Text;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Base
{
    public class Encryption
    {
        public string MD5Encryption(string password)
        {
            //User passwords are kept in the database as md5 hashed. where i activated this algorithm

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encryptedData = Encoding.UTF8.GetBytes(password);
            encryptedData = md5.ComputeHash(encryptedData);
            StringBuilder sb = new StringBuilder();

            foreach (byte b in encryptedData)
            {
                sb.Append(b.ToString("x2").ToLower());
            }


            return sb.ToString();
        }



    }
}
