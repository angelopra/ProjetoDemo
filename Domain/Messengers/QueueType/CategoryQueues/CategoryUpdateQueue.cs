using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Messengers.QueueType.CategoryQueues
{
    public class CategoryUpdateQueue : QueueModelSubscriber
    {
        public CategoryUpdateQueue(string queueName, string exchange, string routingKey, string exchangeType) : base(queueName, exchange, routingKey, exchangeType)
        {
        }
    }
}
