using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Messages message = new Messages() { Name = "Petro", SurName = "Grushevoy", Message = "hi" };
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "Borsoft",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string newMessage = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(newMessage);

                channel.BasicPublish(exchange: "",
                                     routingKey: "Damla",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine($"Message: {message.Name}-{message.SurName}");
            }

            Console.WriteLine("Send Message...");
            Console.ReadLine();
        }
    }
    public class Messages
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Message { get; set; }
    }
}
