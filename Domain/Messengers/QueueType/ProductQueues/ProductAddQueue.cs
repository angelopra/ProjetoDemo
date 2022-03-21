namespace Domain.Messengers.QueueType.ProductQueues
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
