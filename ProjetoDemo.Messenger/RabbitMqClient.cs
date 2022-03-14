using Domain.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDemo.Messenger
{
    public class RabbitMqClient : IMessengerBusClient
    {
        private readonly IConnection _connection;
        public RabbitMqClient(ProducerConnection producerConnection)
        {
            _connection = producerConnection.Connection;
        }

        public void Publish(string queueName, object message, string routingKey, string exchange)
        {
            using (var channel = _connection.CreateModel())
            {
                var body = MakeBody(message);
                channel.ExchangeDeclare(exchange, ExchangeType.Topic, true);
                //channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, null);
                channel.BasicPublish(exchange, routingKey, null, body);
            }
        }

        private byte[] MakeBody(object message)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var payload = JsonConvert.SerializeObject(message, settings);
            return Encoding.UTF8.GetBytes(payload);
        }
    }
}
