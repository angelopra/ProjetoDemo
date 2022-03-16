using Domain.Entities;
using Domain.Interfaces;
using Domain.Messengers;
using Domain.Messengers.QueueType;
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
        private IUnityOfWorkQuery uowQuery;
        public ProductSubscriber(ProducerConnection connection
            ,ProductAddQueue obj
            ,IUnityOfWorkQuery _uowQuery) : base(connection, obj)
        {
            uowQuery = _uowQuery;
        }

        public override void ProcessEvent(string message)
        {
            var obj = JsonConvert.DeserializeObject<Product>(message);
            uowQuery.Product.Add(obj);
            uowQuery.Commit(new CancellationToken());
        }
    }
}
