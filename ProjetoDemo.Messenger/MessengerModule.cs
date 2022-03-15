using Domain.Interfaces;
using Domain.Messengers;
using Domain.Messengers.QueueType;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace ProjetoDemo.Messenger
{
    public static class MessengerModule
    {
        public static void AddMessagerModule(this IServiceCollection services)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            connectionFactory.ClientProvidedName = "app: ProjetoDemo.Messager:event-publisher";

            var connection = connectionFactory.CreateConnection("Messengers");

            services.AddSingleton(new ProductAddQueue("ProductAdd", "ProductExchange", "ProductQueue", ExchangeType.Direct));
            services.AddSingleton(new ProducerConnection(connection));
            services.AddSingleton<IMessengerBusClient, RabbitMqClient>();
        }
    }
}
