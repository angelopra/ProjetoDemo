using Domain.Interfaces;
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

            var connection = connectionFactory.CreateConnection("Messengers");
            services.AddSingleton(new ProducerConnection(connection));
            services.AddSingleton<IMessengerBusClient, RabbitMqClient>();
        }
    }
}
