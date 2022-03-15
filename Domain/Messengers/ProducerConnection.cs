using Domain.Interfaces;
using RabbitMQ.Client;

namespace Domain.Messengers
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
