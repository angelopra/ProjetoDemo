using Domain.Messengers;
using Domain.Messengers.QueueType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ProductBusiness.Subscriber
{
    public class ProductUpdateSubscriber : SubscriberManagerBase<ProductUpdateQueue>
    {
        public ProductUpdateSubscriber(ProducerConnection connection, ProductUpdateQueue obj) : base(connection, obj)
        {
        }

        public override void ProcessEvent(string message)
        {
            throw new NotImplementedException();
        }
    }
}
