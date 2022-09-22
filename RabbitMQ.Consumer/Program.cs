using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Net.Mail;
using Npgsql;
using Hangfire;

namespace RabbitMq.Consumer
{


    public class Program
    {
        [AutomaticRetry(Attempts = 5)]
        public static void SendMail(object userMail) //Sending e-mails from a selected e-mail address to e-mails from the producer
        {
            /// Send Mail ↓
     

            SmtpClient client = new SmtpClient();
            client.Host = "Host"; // Host address // → smtp.live.com
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;


            client.Credentials = new System.Net.NetworkCredential("paycorebitirme94@hotmail.com", "123456789Ca"); 


            MailMessage mm = new MailMessage("paycorebitirme94@hotmail.com", (string)userMail, "ToAllOurClients", "Thank you for choosing us"); 
            mm.IsBodyHtml = false;
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure; // Failure warning 
            client.Send(mm); // Send mail
            

            /// Send Mail ↑
        }

        public static void Update(string message)
        {
            NpgsqlConnection connectionString = new NpgsqlConnection("User ID=postgres;Password=15431543;Server=localhost;Port=5432;Database=postgres;");



            NpgsqlCommand cmd = new NpgsqlCommand(); //Düzelt
            cmd.Connection = connectionString;
            cmd.Connection.Open();
            cmd.CommandText = "update email set status=@p1";


            NpgsqlCommand comnd = new NpgsqlCommand(cmd.CommandText, connectionString);
            comnd.Parameters.AddWithValue("@p1", message);

            comnd.ExecuteNonQuery();
            connectionString.Close();

        }//Where email status is edited



        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "Mail", durable: false, exclusive: false, autoDelete: false, arguments: null);


                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {

                    try
                    {
                        byte[] body = ea.Body.ToArray();
                        string message = Encoding.UTF8.GetString(body);
                        Object mail = JsonConvert.DeserializeObject<Object>(message);

                        Console.WriteLine($"Value = {(string)mail}");
                        Console.WriteLine("E mail received.");
                        SendMail(mail);
                       
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                  
                    Update("Received");
                    Thread.Sleep(1000);
                };
                channel.BasicConsume(queue: "Mail", autoAck: true, consumer: consumer);

            }


            Console.ReadLine();

        }
    }
}
