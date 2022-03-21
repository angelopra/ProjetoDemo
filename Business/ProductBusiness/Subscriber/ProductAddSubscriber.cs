using Domain.Entities;
using Domain.Interfaces;
using Domain.Messengers;
using Domain.Messengers.QueueType;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;

namespace Business.ProductBusiness.Subscriber
{
    public class ProductAddSubscriber : SubscriberManagerBase<ProductAddQueue>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IUnityOfWorkQuery uowQuery;

        public ProductAddSubscriber(
            ProducerConnection connection
            ,ProductAddQueue obj
            ,IServiceScopeFactory serviceScopeFactory
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

                var getCategory = uowQuery.Category.Where(c => c.Id == obj.IdCategory).FirstOrDefault();
                if (getCategory != null)
                {
                    obj.Category = null;
                }

                uowQuery.Product.Add(obj);
                uowQuery.Commit(cancellationToken);
            }
        }
    }
}
