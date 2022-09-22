using Newtonsoft.Json;
using Npgsql;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Configuration;



namespace RabbitMq.Producer
{


    public class Program
    {



        private static List<object> GetMailList(string instance) //I pull the mailing list from the email table I opened in the database and fill it in the list.
        {

            NpgsqlConnection connectionString = new NpgsqlConnection("User ID=postgres;Password=15431543;Server=localhost;Port=5432;Database=postgres;");



            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connectionString;
            cmd.Connection.Open();
            cmd.CommandText = "select email from email";
            List<object> mail = new List<object>();
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                mail.Add(dr.GetValue(0));
            }
            return mail;

        }


        public static void Update(string mail, int count) //The point where I change the status of the mails. The status is changed as soon as it is added to the queue.
        {
            NpgsqlConnection connectionString = new NpgsqlConnection("User ID=postgres;Password=15431543;Server=localhost;Port=5432;Database=postgres;");



            NpgsqlCommand cmd = new NpgsqlCommand(); //Düzelt
            cmd.Connection = connectionString;
            cmd.Connection.Open();
            cmd.CommandText = "update email set status=@p1";

            
            NpgsqlCommand comnd = new NpgsqlCommand(cmd.CommandText, connectionString);
            comnd.Parameters.AddWithValue("@p1", "Mail waiting in queue");
            
            comnd.ExecuteNonQuery();
            connectionString.Close();
            
        }


        static void Main(string[] args)
        {
            string InstanceID = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper();

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "Mail", durable: false, exclusive: false, autoDelete: false, arguments: null);

                Console.WriteLine($"Adding registered e-mails to the queue from Server={InstanceID}");
                List<object> mailList = GetMailList(InstanceID);
                int count = 0;
                foreach ( var item in mailList)
                {
                    
                    count++;
                    string message = JsonConvert.SerializeObject(item);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: "Mail", basicProperties: null, body: body);

                    Console.WriteLine($"Server = {InstanceID} Mail: {(string)item} ");
                    Update((string)item, count);
                    Thread.Sleep(2000);
                }
            }



            

            Console.WriteLine("All Mails Queued ...");
            Console.ReadLine();

        }
    }
}
