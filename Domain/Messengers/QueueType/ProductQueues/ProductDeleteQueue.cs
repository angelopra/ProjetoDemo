﻿namespace Domain.Messengers.QueueType.ProductQueues
{
    public class ProductDeleteQueue : QueueModelSubscriber
    {
        public ProductDeleteQueue(
            string queueName,
            string exchange,
            string routingKey,
            string exchangeType) : base(queueName, exchange, routingKey, exchangeType)
        { }
    }
}
