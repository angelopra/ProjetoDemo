using Domain.Interfaces;
using Domain.Messengers;
using Domain.Messengers.QueueType;
using Microsoft.Extensions.Hosting;
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
        public ProductSubscriber(ProducerConnection connection, ProductAddQueue obj) : base(connection, obj)
        {

        }

        public virtual void ProcessEvent(string message)
        {
            Console.WriteLine("chamou no ProductSubscriber");
        }
    }
}
