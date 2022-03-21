using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Messengers.QueueType.ProductQueues
{
    public class ProductRemoveQueue : QueueModelSubscriber
    {
        public ProductRemoveQueue(
            string queueName, 
            string exchange, 
            string routingKey, 
            string exchangeType) : base(queueName, exchange, routingKey, exchangeType)
        {
        }
    }
}
