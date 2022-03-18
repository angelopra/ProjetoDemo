﻿using Domain.Messengers;
using Domain.Messengers.QueueType;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDemo.Hangfire
{
    public abstract class HangfireSubscriber<T>
    {
        public IModel _channel;
        private readonly IConnection _connection;
        public QueueModelSubscriber _obj;

        public HangfireSubscriber(ProducerConnection connection, T obj)
        {
            _connection = connection.Connection;
            _obj = obj as QueueModelSubscriber;

            Console.WriteLine("---> listening on RabbitMQ");
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: _obj.Exchange, type: _obj.ExchangeType);
            _channel.QueueDeclare(queue: _obj.QueueName, durable: true, exclusive: false, autoDelete: false, null);
            _channel.QueueBind(queue: _obj.QueueName, exchange: _obj.Exchange, routingKey: _obj.RoutingKey);
        }

        public async Task ExecuteAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    Console.WriteLine("foi");
                    var consumer = new EventingBasicConsumer(_channel);
                    consumer.Received += (ModuleHandle, ea) =>
                    {
                        Console.WriteLine("---> event received");

                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body.ToArray());

                        // event processor
                        ProcessEvent(message);

                        _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    };

                    _channel.BasicConsume(queue: _obj.QueueName, autoAck: false, consumer: consumer);
                    return Task.CompletedTask;
                }
                catch (Exception err)
                {
                    // se der algum erro ele escreve a mensagem em um arquivo de texto ou algo do tipo pra se ter o log
                    throw err;
                }
            });
        }
        public abstract void ProcessEvent(string message);

        public void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }
    }
}