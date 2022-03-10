using RabbitMQ.Client;

namespace ProjetoDemo.Messenger
{
    public class ProducerConnection
    {
        public IConnection Connection { get; private set; }

        public ProducerConnection(IConnection connection)
        {
            Connection = connection;
        }
    }
}
