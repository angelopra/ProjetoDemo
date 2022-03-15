using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Messengers.QueueType
{
    public class ProductAddQueue : QueueModelSubscriber
    {
        public ProductAddQueue(
            string queueName, 
            string exchange, 
            string routingKey, 
            string exchangeType) : base(queueName, exchange, routingKey, exchangeType)
        { }
    }
}
