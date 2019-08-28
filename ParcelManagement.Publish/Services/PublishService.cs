using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParcelManagement.Domain.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace ParcelManagement.Queue.Services
{
    public class PublishService : IPublishService
    {
        public PublishService()
        {
        }

        public async Task PublishAsync(string queueName, Parcel parcel)
        {

            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(
                            queue: queueName,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                        String jsonData = JsonConvert.SerializeObject(parcel);
                        var body = Encoding.UTF8.GetBytes(jsonData);
                        channel.BasicPublish(
                            exchange: "",
                            routingKey: queueName,
                            basicProperties: null,
                            body: body);

                        Console.WriteLine($"New parcel added to {queueName}, Sent {jsonData}");
                    }
                }
            }
            catch (BrokerUnreachableException ex)
            {
                Console.WriteLine(ex.InnerException);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Data.Keys);
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
