﻿using Domain.Interfaces;
using RabbitMQ.Client;

namespace ProjetoDemo.Messenger
{
    public class ProducerConnection : IProducerConnection
    {
        public IConnection Connection { get; set; }

        public ProducerConnection(IConnection connection)
        {
            Connection = connection;
        }
    }
}
