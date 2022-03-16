using Domain.Interfaces;
using Domain.Messengers;
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
    public class RabbitMqClient : IMessengerBusClient, IDisposable
    {
        private readonly IConnection _connection;
        private IModel _channel;
        public RabbitMqClient(ProducerConnection producerConnection)
        {
            _connection = producerConnection.Connection;
            _channel = _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }

        public void Publish(string queueName, object message, string routingKey, string exchange)
        {
            // aqui é criado um canal pra cada vez que o publish é chamado, mas na documentação do rabbitmq é recomendado a reutilização de canais
            var body = MakeBody(message);
            //channel.ExchangeDeclare(exchange, ExchangeType.Topic, true);
            //channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, null);
            _channel.BasicPublish(exchange, routingKey, null, body);
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
