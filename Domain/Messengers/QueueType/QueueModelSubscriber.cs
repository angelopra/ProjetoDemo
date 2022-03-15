namespace Domain.Messengers.QueueType
{
    public class QueueModelSubscriber
    {
        public string QueueName { get; private set; }
        public string Exchange { get; private set; }
        public string RoutingKey { get; private set; }
        public string ExchangeType { get; private set; }

        public QueueModelSubscriber
            (string queueName,
            string exchange,
            string routingKey,
            string exchangeType)
        {
            QueueName = queueName;
            Exchange = exchange;
            RoutingKey = routingKey;
            ExchangeType = exchangeType;
        }
    }
}
