using Microsoft.Extensions.Options;
using MotorcycleRental.Domain.Interfaces.Repositories.MongoDb;
using MotorcycleRental.MailServiceConsumer.Config;
using MotorcycleRental.MailServiceConsumer.Models;
using MotorcycleRental.MailServiceConsumer.Service;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MotorcycleRental.MailServiceConsumer.ConsumerService
{
    public class SendMailConsumer : BackgroundService
    {
        private readonly IMotorcycleMongoRepository _mongoRepository;
        private readonly MailQueueSettings _mailQueueSettings;

        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly INotifyService _notifyService;
        public SendMailConsumer(IOptions<MailQueueSettings> mailQueueSettings,
                                IMotorcycleMongoRepository mongoRepository,
                                INotifyService notifyService)
        {
            _mongoRepository = mongoRepository;
            _mailQueueSettings = mailQueueSettings.Value;
            Initialize(ref _connection, ref _channel);
            _notifyService = notifyService;

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            EventingBasicConsumer consumer = new EventingBasicConsumer(_channel);
            bool isValid = false;

            consumer.Received += async (sender, eventArgs) =>
            {
                var contentArray = eventArgs.Body.ToArray();

                var contentString = Encoding.UTF8.GetString(contentArray);
                EmailVm email = new EmailVm();

                try
                {
                    email = JsonConvert.DeserializeObject<EmailVm>(contentString);
                    isValid = true;
                }
                catch (Exception ex)
                {
                    _channel.BasicReject(eventArgs.DeliveryTag, false);
                }

                try
                {
                    email = JsonConvert.DeserializeObject<EmailVm>(contentString);
                    _notifyService.SendMail(email.Emails, email.Subject, email.Body, true);
                }
                catch (Exception)
                {
                    _channel.BasicNack(eventArgs.DeliveryTag, false, true);
                }
                _channel.BasicAck(eventArgs.DeliveryTag, false);

            };

            _channel.BasicConsume(_mailQueueSettings.Queue, false, consumer);

            return Task.CompletedTask;
        }

        private void Initialize(ref IConnection _connection, ref IModel _channel)
        {
            var connectionFactory = new ConnectionFactory() { };
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(_mailQueueSettings.Exchange, _mailQueueSettings.TypeExchange, true, false);
            _channel.QueueDeclare(_mailQueueSettings.Queue, true, false, false);
            _channel.QueueBind(_mailQueueSettings.Queue, _mailQueueSettings.Exchange, _mailQueueSettings.RoutingKey, null);

            _channel.BasicQos(0, _mailQueueSettings.PrefetchCount, false);
        }
    }
}
