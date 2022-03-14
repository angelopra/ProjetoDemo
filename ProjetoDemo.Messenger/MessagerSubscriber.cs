using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjetoDemo.Messenger
{
    public class MessagerSubscriber : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private IModel _channel;
        private string _queueName;

        public MessagerSubscriber( IConfiguration configuration, ProducerConnection producerConnection)
        {
            _configuration = configuration;
            _connection = producerConnection.Connection;
            Initialize();
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ModuleHandle, ea) =>
            {
                Console.WriteLine("---> event received");

                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());

                //_eventProcessor.ProcessEvent(message);
            };

            _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
            return Task.CompletedTask;
        }

        private void Initialize()
        {
            using (var channel = _connection.CreateModel())
            {
                _channel.ExchangeDeclare(exchange: "teste", type: ExchangeType.Fanout);
                _queueName = _channel.QueueDeclare().QueueName;
                _channel.QueueBind(queue: _queueName, exchange: "teste", routingKey: "");
            }
        }
    }
}
