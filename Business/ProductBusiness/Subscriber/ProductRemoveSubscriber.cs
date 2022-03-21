using Domain.Entities;
using Domain.Interfaces;
using Domain.Messengers;
using Domain.Messengers.QueueType.ProductQueues;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.ProductBusiness.Subscriber
{
    public class ProductRemoveSubscriber : SubscriberManagerBase<ProductRemoveQueue>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IUnityOfWorkQuery uowQuery;
        public ProductRemoveSubscriber(ProducerConnection connection, ProductRemoveQueue obj, IServiceScopeFactory serviceScopeFactory) : base(connection, obj)
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

                var product = uowQuery.Product.Where(c => c.Id == obj.Id).FirstOrDefault();
                if (product == null)
                {
                    throw new Exception("Product doesn't exist");
                }

                uowQuery.Product.Remove(product);
                uowQuery.Commit(cancellationToken);
            }
        }
    }
}
