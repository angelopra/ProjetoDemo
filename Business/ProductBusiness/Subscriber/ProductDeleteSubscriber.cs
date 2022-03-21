using Domain.Entities;
using Domain.Interfaces;
using Domain.Messengers;
using Domain.Messengers.QueueType;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace Business.ProductBusiness.Subscriber
{
    public class ProductDeleteSubscriber : SubscriberManagerBase<ProductDeleteQueue>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IUnityOfWorkQuery uowQuery;

        public ProductDeleteSubscriber(
            ProducerConnection connection
            , ProductDeleteQueue obj
            , IServiceScopeFactory serviceScopeFactory
            ) : base(connection, obj)
        {
            _serviceScopeFactory = serviceScopeFactory;
            var scope = _serviceScopeFactory.CreateScope();
            uowQuery = scope.ServiceProvider.GetService<IUnityOfWorkQuery>();
        }

        public override void ProcessEvent(string message)
        {
            if (!String.IsNullOrEmpty(message))
            {
                var cancellationToken = new CancellationToken();
                var obj = JsonConvert.DeserializeObject<Product>(message);

                uowQuery.Product.Remove(obj);
                uowQuery.Commit(cancellationToken);
            }
        }
    }
}
