using Microsoft.Extensions.Logging;
using MT.Backend.Challenge.Domain.Constants;
using MT.Backend.Challenge.Domain.Entities;
using MT.Backend.Challenge.Domain.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

namespace MT.Backend.Challenge.Application.Commands.Motorcycles.AddMotorcycle
{
    public class MotorcycleRegisteredConsumer
    {
        private readonly IWriteRepository<MotorcycleNotification> NotificationRepository;
        private readonly ILogger<MotorcycleRegisteredConsumer> Logger;
        private readonly string QueueName = "motorcycle_registered";
        private readonly string HostName = "localhost";

        public MotorcycleRegisteredConsumer(IWriteRepository<MotorcycleNotification> notificationRepository, ILogger<MotorcycleRegisteredConsumer> logger)
        {
            NotificationRepository = notificationRepository;
            Logger = logger;
        }

        public void StartConsuming()
        {
            var factory = new ConnectionFactory() { HostName = HostName };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: QueueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var motorcycle = JsonSerializer.Deserialize<Motorcycle>(message);

                if (motorcycle != null && motorcycle.Year == 2024)
                {
                    var notification = new MotorcycleNotification
                    {
                        MotorcycleId = motorcycle.Id,
                        Message = $"Moto do ano {motorcycle.Year} cadastrada.",
                        CreatedAt = DateTime.Now
                    };
                    await NotificationRepository.Add(notification);
                    var logMessage = $"{ServiceConstants.NotificationStored}: {motorcycle.Id}";
                    Logger.LogInformation(logMessage);
                }
            };

            channel.BasicConsume(queue: QueueName,
                                 autoAck: true,
                                 consumer: consumer);
        }
    }
}
