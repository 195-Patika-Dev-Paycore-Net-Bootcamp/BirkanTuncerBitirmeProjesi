using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Security.Cryptography;

namespace BirkanTuncer_PayCore_BitirmeProjesi.TestX
{
    public class MD5Test
    {
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

        [Fact]
        public void MD5_Test_1()
        {
            string password = "123456789";
            string expected = "25f9e794323b453885f5181f1b624d0b";


            
            byte[] encryptedData = Encoding.UTF8.GetBytes(password);
            encryptedData = md5.ComputeHash(encryptedData);
            StringBuilder sb = new StringBuilder();

            foreach (byte b in encryptedData)
            {
                sb.Append(b.ToString("x2").ToLower());
            }

            string result = sb.ToString();
            Assert.Equal(expected, result);

        }
        [Fact]
        public void MD5_Test_2()
        {
            string password = "987654321";
            string expected = "6ebe76c9fb411be97b3b0d48b791a7c9";


            byte[] encryptedData = Encoding.UTF8.GetBytes(password);
            encryptedData = md5.ComputeHash(encryptedData);
            StringBuilder sb = new StringBuilder();

            foreach (byte b in encryptedData)
            {
                sb.Append(b.ToString("x2").ToLower());
            }

            string result = sb.ToString();
            Assert.Equal(expected, result);

        }
        [Fact]
        public void MD5_Test_3()
        {
            string password = "paycorepaycore";
            string expected = "a88f73249ea256d2a12b8d8a32ee9a9c";


            byte[] encryptedData = Encoding.UTF8.GetBytes(password);
            encryptedData = md5.ComputeHash(encryptedData);
            StringBuilder sb = new StringBuilder();

            foreach (byte b in encryptedData)
            {
                sb.Append(b.ToString("x2").ToLower());
            }

            string result = sb.ToString();
            Assert.Equal(expected, result);

        }

        





    }
}
