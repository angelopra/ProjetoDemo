using Domain.Entities;
using Domain.Interfaces;
using Domain.Messengers;
using Domain.Messengers.QueueType;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.ProductBusiness.Subscriber
{
    public class ProductSubscriber : InitializeSubscriber<ProductAddQueue>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ProductSubscriber(
            ProducerConnection connection
            ,ProductAddQueue obj
            ,IServiceScopeFactory serviceScopeFactory
            ) : base(connection, obj)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override void ProcessEvent(string message)
        {
            if (!String.IsNullOrEmpty(message))
            {
                var cancellationToken = new CancellationToken();
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var uowQuery = scope.ServiceProvider.GetService<IUnityOfWorkQuery>();

                    var obj = JsonConvert.DeserializeObject<Product>(message);
                    uowQuery.Product.Add(obj);
                    uowQuery.Commit(cancellationToken);
                }
            }
        }
    }
}
