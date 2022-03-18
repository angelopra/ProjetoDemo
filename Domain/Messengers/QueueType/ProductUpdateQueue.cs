﻿namespace Domain.Messengers.QueueType
{
    public class ProductUpdateQueue : QueueModelSubscriber
    {
        public ProductUpdateQueue(
            string queueName,
            string exchange,
            string routingKey,
            string exchangeType) : base(queueName, exchange, routingKey, exchangeType)
        { }
    }
}
