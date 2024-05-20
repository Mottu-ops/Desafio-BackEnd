
using Microsoft.Extensions.Options;
using MotorcycleRental.DeliveryManagementService.Api.Config;
using MotorcycleRental.DeliveryManagementService.Api.Config.Models;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MotorcycleRental.DeliveryManagementService.Api.ServiceConsumer
{
    public class RegisterUserService : BackgroundService
    {   
        private readonly RegisterQueueSettings _registerQueueSettings;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        public  IServiceProvider _serviceProvider;
        public RegisterUserService(IOptions<RegisterQueueSettings> registerQueueSettings,
                                   IServiceProvider serviceProvider)
        {        
            _registerQueueSettings = registerQueueSettings.Value;
            Initialize(ref _connection, ref _channel);
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            bool isValid = false;

            consumer.Received += async (sender, eventArgs) =>
            {
                var contentArray = eventArgs.Body.ToArray();

                var contentString = Encoding.UTF8.GetString(contentArray);
                RegisterNewUser newUser;
                try
                {
                    newUser = JsonConvert.DeserializeObject<RegisterNewUser>(contentString);
                    isValid = true;
                }
                catch (Exception ex)
                {
                    _channel.BasicReject(eventArgs.DeliveryTag, false);                    
                }

                if (isValid)
                {
                    try
                    {
                        newUser = JsonConvert.DeserializeObject<RegisterNewUser>(contentString);
                        await ProcessMessage(newUser);
                        _channel.BasicAck(eventArgs.DeliveryTag, false);
                    }
                    catch (Exception)
                    {
                        _channel.BasicNack(eventArgs.DeliveryTag, false, true);
                        //Enviar email para Admin ou o proprio usuario.
                    }
                    
                }
            };

            _channel.BasicConsume(_registerQueueSettings.Queue, false, consumer);

            return Task.CompletedTask;
        }

        private async Task ProcessMessage(RegisterNewUser newUser)
        {
            var deliveryman = new Deliveryman(newUser.Id, newUser.Name, newUser.Email, newUser.Cnpj,
                                              newUser.DateOfBirth, newUser.DriverLicenseNumber, newUser.DriverLicenseType);

            using (var scope = _serviceProvider.CreateScope())
            {
                
                var _deliverymanRepository = scope.ServiceProvider.GetService<IDeliverymanRepository>();
                try
                {
                    await _deliverymanRepository.AddAsync(deliveryman);
                }
                catch (Exception ex)
                {
                }
                
            }
        }

        private void Initialize(ref IConnection _connection, ref IModel _channel)
        {
            var connectionFactory = new ConnectionFactory() { };
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(_registerQueueSettings.Exchange, _registerQueueSettings.TypeExchange, true, false);
            _channel.QueueDeclare(_registerQueueSettings.Queue, true, false, false);
            _channel.QueueBind(_registerQueueSettings.Queue, _registerQueueSettings.Exchange, _registerQueueSettings.RoutingKey, null);

            _channel.BasicQos(0, _registerQueueSettings.PrefetchCount, false);
        }
    }
}
