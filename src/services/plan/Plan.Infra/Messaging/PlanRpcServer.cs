﻿using System.Text;
using BusConnections.Core.Model;
using BusConnections.Events;
using BusConnections.Events.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Plan.Domain.Entity;
using Plan.Infra.Context;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Plan.Infra.Messaging
{
    public class PlanRpcServer
    {
        private readonly IBusConnection _persistentConnection;
        private readonly IDbContextFactory<PlanContext> _context;
        public PlanRpcServer(IBusConnection persistentConnection, IServiceScopeFactory factory, IDbContextFactory<PlanContext> context)
        {
            _persistentConnection = persistentConnection;
            _context = context;
        }
        public void Consume(string queue)
        {
            if (!_persistentConnection.IsConnected)
                _persistentConnection.TryConnect();

            var channel = _persistentConnection.CreateModel();

            var args = new Dictionary<string, object> { { "x-single-active-consumer", true } };

            channel.QueueDeclare(
                   queue: queue,
                   durable: true,
                   exclusive: false,
                   autoDelete: false,
                   arguments: args);
            channel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);
            consumer.Received += (model, ea) =>
            {
                ReceivedEventAsync(model, ea, channel);
            };
        }
        private async Task ReceivedEventAsync(object sender, BasicDeliverEventArgs ea, IModel channel)
        {
            string response = null;

            var props = ea.BasicProperties;
            var replyProps = channel.CreateBasicProperties();
            replyProps.CorrelationId = props.CorrelationId;

            try
            {
                var message = Encoding.UTF8.GetString(ea.Body.Span);
                var data = JsonConvert.DeserializeObject<Request>(message);
                List<RentPlan> plans = new List<RentPlan>();
                if (ea.RoutingKey == $"{EventBusConstants.RdcPublishQueue}")
                {
                    string method = data!.Method;
                    switch (method)
                    {
                        case "GetPlan":
                            int planId = data.Payload["Id"];
                            var dbContext = _context.CreateDbContext();
                            plans = await dbContext.Plans.AsNoTracking().Where(x => x.Id == planId).ToListAsync();
                            break;
                    }
                }
                response = JsonConvert.SerializeObject(new Response() { Success = true, Payload = plans.FirstOrDefault()});
            }
            catch (Exception ex)
            {
                //logging
                response = JsonConvert.SerializeObject(new Response() { Success = false, ErrorMessage = ex.Message});
            }
            finally
            {
                var responseBytes = Encoding.UTF8.GetBytes(response);
                channel.BasicPublish(
                    exchange: "",
                    routingKey: props.ReplyTo,
                    basicProperties: replyProps,
                    body: responseBytes);
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            }
        }
        public void Disconnect()
        {
            _persistentConnection.Dispose();
        }
    }
}