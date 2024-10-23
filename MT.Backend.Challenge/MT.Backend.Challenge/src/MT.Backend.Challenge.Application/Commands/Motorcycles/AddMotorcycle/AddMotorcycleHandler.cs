using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MT.Backend.Challenge.Application.Helpers;
using MT.Backend.Challenge.Domain.Constants;
using MT.Backend.Challenge.Domain.Entities;
using MT.Backend.Challenge.Domain.Entities.Response;
using MT.Backend.Challenge.Domain.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MT.Backend.Challenge.Application.Commands.Motorcycles.AddMotorcycle
{
    public class AddMotorcycleHandler : IRequestHandler<AddMotorcycleRequest, AddMotorcycleResponse>
    {
        private ILogger<AddMotorcycleHandler> Logger { get; }
        private IWriteRepository<Motorcycle> Repository { get; }
        private IMapper Mapper { get; }

        private readonly HandleResponseExceptionHelper HandleResponseExceptionHelper;
        private readonly string QueueName = "motorcycle_registered";
        private readonly string HostName = "rabbitmq";

        public AddMotorcycleHandler(
            IWriteRepository<Motorcycle> repository,
            ILogger<AddMotorcycleHandler> logger,
            IMapper mapper
        )
        {
            Logger = logger;
            Repository = repository;
            Mapper = mapper;
            HandleResponseExceptionHelper = new HandleResponseExceptionHelper(logger);
        }

        public async Task<AddMotorcycleResponse> Handle(AddMotorcycleRequest request, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Handling AddMotorcycleRequest");
            var response = new AddMotorcycleResponse()
            {
                Message = ServiceConstants.DefaultSuccessMessage,
                StatusCode = 201
            };

            try
            {
                var entity = Mapper.Map<Motorcycle>(request);
                entity.CreatedAt = DateTime.Now;
                var result = await Repository.Add(entity);

                if (result.Success)
                {
                    // Enviar mensagem para fila
                    PublishMotorcycleRegisteredEvent(entity);
                }
                else
                {
                    HandleResponseExceptionHelper.HandleResponseException(response, result);
                }

            }
            catch (Exception ex)
            {
                HandleResponseExceptionHelper.HandleException(response, ex);
            }

            Logger.LogInformation($"{ServiceConstants.MotorcycleService} Finish Handle request");
            return response;
        }

        private void PublishMotorcycleRegisteredEvent(Motorcycle motorcycle)
        {
            // colocar no secrets
            var factory = new ConnectionFactory() { 
                HostName = HostName,
                UserName = "mtuser",
                Password = "mt2024"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: QueueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var message = JsonSerializer.Serialize(motorcycle);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: QueueName,
                                     basicProperties: null,
                                     body: body);

                var logMessage = $"{ServiceConstants.ItemQueueRegistred}: {motorcycle.Id}";
                Logger.LogInformation(logMessage);
            }
        }
    }


}
