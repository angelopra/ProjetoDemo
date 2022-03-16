using Domain.Messengers.QueueType;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Messengers
{
    public abstract class InitializeSubscriber<T> : BackgroundService
    {
        public IModel _channel;
        private readonly IConnection _connection;
        public QueueModelSubscriber _obj;
        private Timer _timer;
        private string _queueName;


        public InitializeSubscriber(ProducerConnection connection, T obj)
        {
            _connection = connection.Connection;
            _obj = obj as QueueModelSubscriber;

            Console.WriteLine("---> listening on RabbitMQ");
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: _obj.Exchange, type: _obj.ExchangeType);
            _channel.QueueDeclare(queue: _obj.QueueName, durable: true, exclusive: false, autoDelete: false, null);
            _channel.QueueBind(queue: _obj.QueueName, exchange: _obj.Exchange, routingKey: _obj.RoutingKey);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
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
        }

        //public Task StartAsync(CancellationToken cancellationToken)
        //{
        //    _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(1));

        //    return Task.CompletedTask;
        //}

        //public Task StopAsync(CancellationToken cancellationToken)
        //{
        //    Dispose();
        //    return Task.CompletedTask;
        //}

        //private void DoWork(object state)
        //{
        //    try
        //    {
        //        var consumer = new EventingBasicConsumer(_channel);
        //        consumer.Received += (ModuleHandle, ea) =>
        //        {
        //            Console.WriteLine("---> event received");

        //            var body = ea.Body;
        //            var message = Encoding.UTF8.GetString(body.ToArray());

        //            // event processor
        //            ProcessEvent(message);

        //            //_channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        //        };
        //        _channel.BasicConsume(queue: _obj.QueueName, autoAck: true, consumer: consumer);
        //    }
        //    catch (Exception err)
        //    {
        //        // se der algum erro ele escreve a mensagem em um arquivo de texto ou algo do tipo pra se ter o log
        //        throw err;
        //    }
        //}
        public virtual void ProcessEvent(string message)
        {
            Console.WriteLine("implementa essa bagaça");
        }
        public void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
            base.Dispose();
        }
    }
}
