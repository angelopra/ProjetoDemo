using Domain.Interfaces;
using Domain.Messengers;
using Domain.Messengers.QueueType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDemo.Hangfire
{
    public class ProductSubscriber : HangfireSubscriber<ProductAddQueue>, IProductSubscriber
    {
        public ProductSubscriber(ProducerConnection connection, ProductAddQueue obj) : base(connection, obj)
        {
        }

        public override void ProcessEvent(string message)
        {
            Console.WriteLine("chegou no ProductSubscriber do Hangfire");
            Console.WriteLine(message);
        }
    }
}


