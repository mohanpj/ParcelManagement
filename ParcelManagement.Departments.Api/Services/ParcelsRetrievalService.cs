using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using ParcelManagement.Domain.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ParcelManagement.Departments.Api.Services
{
    public class ParcelsRetrievalService : IParcelsRetrievalService
    {
        public ParcelsRetrievalService()
        {
        }

        public List<Parcel> GetParcelsByDepartment(string departmentName)
        {
            List<Parcel> parcels = new List<Parcel>();
            string queueName = string.Empty;
            switch (departmentName)
            {
                case "insurance":
                    queueName = "parcels.department.insurance";
                    break;
                case "mail":
                    queueName = "parcels.department.mail";
                    break;
                case "regular":
                    queueName = "parcels.department.regular";
                    break;
                case "heavy":
                    queueName = "parcels.department.heavy";
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(queueName))
            {
                parcels = this.GetParcels(queueName);
            }
            return parcels;
        }

        public List<Parcel> GetParcels(string queueName)
        {
            List<Parcel> parcels = new List<Parcel>();
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
                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += (model, ea) =>
                        {
                            var body = ea.Body;
                            var parcelJson = Encoding.UTF8.GetString(body);
                            parcels.Add(JsonConvert.DeserializeObject<Parcel>(parcelJson));
                            Console.WriteLine($"Parcel from {queueName} Received {parcelJson}");
                        };
                        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
            }
            return parcels;
        }
    }
}
