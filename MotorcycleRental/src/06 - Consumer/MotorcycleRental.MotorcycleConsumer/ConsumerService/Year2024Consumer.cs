using Microsoft.Extensions.Options;
using MotorcycleRental.Domain.Entities.Mongo;
using MotorcycleRental.Domain.Interfaces.Repositories.MongoDb;
using MotorcycleRental.MailServiceConsumer.Config;
using MotorcycleRental.MotorcycleConsumer.Config;
using MotorcycleRental.MotorcycleConsumer.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqMessage.Service;
using System.Text;

namespace MotorcycleRental.MotorcycleConsumer.ConsumerService
{
    public class Year2024Consumer : BackgroundService
    {
        private readonly IMotorcycleMongoRepository _mongoRepository;
        private readonly RabbitMqSettings _rabbitMqSettings;
        private readonly MailQueueSettings _mailQueueSettings;
        private readonly EmailList _emailList;

        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IRabbitMqPublish _rabbitMqPublish;


        public Year2024Consumer(IOptions<RabbitMqSettings> rabbitMqSettings,
                    IOptions<MailQueueSettings> mailQueueSettings,
                    IOptions<EmailList> emailList,
                    IMotorcycleMongoRepository mongoRepository,
                    IRabbitMqPublish rabbitMqPublish)
        {
            _mongoRepository = mongoRepository;
            _rabbitMqSettings = rabbitMqSettings.Value;
            _mailQueueSettings = mailQueueSettings.Value;
            _emailList = emailList.Value;
            Initialize(ref _connection, ref _channel);
            _rabbitMqPublish = rabbitMqPublish;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            bool isValid = false;

            consumer.Received += async (sender, eventArgs) =>
            {
                var contentArray = eventArgs.Body.ToArray();

                var contentString = Encoding.UTF8.GetString(contentArray);
                MotorcycleCreated motorcycle = new MotorcycleCreated();
                try
                {
                    motorcycle = JsonConvert.DeserializeObject<MotorcycleCreated>(contentString);
                    isValid = true;
                }
                catch (Exception ex)
                {
                    _channel.BasicReject(eventArgs.DeliveryTag, false);
                    await SendMailPublish(contentString, "Invalid Object", true, true);
                }

                if (isValid)
                {
                    try
                    {
                        motorcycle = JsonConvert.DeserializeObject<MotorcycleCreated>(contentString);
                        await ProcessMessage(motorcycle);
                        await SendMailPublish($"Id: {motorcycle.Id} - Plate: {motorcycle.Plate} - Year: {motorcycle.Year}.", "New Motocycle created", true, false);
                    }
                    catch (Exception)
                    {
                        _channel.BasicNack(eventArgs.DeliveryTag, false, true);
                        await SendMailPublish(contentString, "Error processing", true, true);
                    }
                    _channel.BasicAck(eventArgs.DeliveryTag, false);
                }
            };

            _channel.BasicConsume(_rabbitMqSettings.Queue, false, consumer);

            return Task.CompletedTask;
        }

        private async Task ProcessMessage(MotorcycleCreated motorcycle)
        {
            await _mongoRepository.AddAsync(ToEntity(motorcycle));
        }

        private async Task SendMailPublish(string _object, string subject, bool isHtml, bool isError)
        {
            var message = isError ? Layout.GetMessageError(_object, "motorcycle") : Layout.GetMessageSucess(_object, "motorcycle");
            var email = new EmailVm(_emailList.List, subject, message, isHtml);

            _rabbitMqPublish.Publish(email, _mailQueueSettings.RoutingKey, _mailQueueSettings.Queue, _mailQueueSettings.Exchange);
        }

        private void Initialize(ref IConnection _connection, ref IModel _channel)
        {
            var connectionFactory = new ConnectionFactory() { };
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(_rabbitMqSettings.Exchange, _rabbitMqSettings.TypeExchange, true, false);
            _channel.QueueDeclare(_rabbitMqSettings.Queue, true, false, false);
            _channel.QueueBind(_rabbitMqSettings.Queue, _rabbitMqSettings.Exchange, _rabbitMqSettings.RoutingKey, null);

            _channel.BasicQos(0, _rabbitMqSettings.PrefetchCount, false);
        }

        private MotorcycleMgDb ToEntity(MotorcycleCreated motorcycle)
        {
            return new MotorcycleMgDb(motorcycle.Id,
                                            motorcycle.Year,
                                            motorcycle.Model,
                                            motorcycle.Plate,
                                            motorcycle.CreatedAt,
                                            motorcycle.IsActived);
        }
    }

    public class MotorcycleCreated()
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActived { get; set; }
    }
}
