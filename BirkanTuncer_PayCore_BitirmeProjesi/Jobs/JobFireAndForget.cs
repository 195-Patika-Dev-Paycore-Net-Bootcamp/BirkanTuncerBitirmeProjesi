using Hangfire;
using Hangfire.Server;
using System;
using System.Net.Mail;
using System.Text;

namespace BirkanTuncer_PayCore_BitirmeProjesi
{
    public static class JobFireAndForget
    {
        public static string Run()
        {
            Console.WriteLine("FireAndForget");
            return "FireAndForget";
        }

        
        public static string Welcome(string email)
        {
            
            return email + " Thank you for registering, welcome aboard";
        }

        [AutomaticRetry(Attempts = 5)]
        public static string Login(string email)
        {
            return email + " You have just logged into the system, welcome !";
        }
        public static string OfferAccept(string email)
        {
            return email + " You accepted the offer made !";
        }
        public static string OfferReject(string email)
        {
            return email + " You rejected the offer made !";
        }
        public static string Buy(string email)
        {
            return email + " You bought the product !";
        }
        public static string CreateProduct(string email)
        {
            return email + " You have created a product !";
        }
        public static string DeleteProduct(string email)
        {
            return email + " You deleted the product !";
        }

    }
}
