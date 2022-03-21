using Domain.Entities;
using Domain.Interfaces;
using Domain.Messengers;
using Domain.Messengers.QueueType.CategoryQueues;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.CategoryBusiness.Subscriber
{
    public class CategoryUpdateSubscriber : SubscriberManagerBase<CategoryUpdateQueue>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IUnityOfWorkQuery uowQuery;
        public CategoryUpdateSubscriber(ProducerConnection connection
            , CategoryUpdateQueue obj
            , IServiceScopeFactory serviceScopeFactory) : base(connection, obj)
        {
            _serviceScopeFactory = serviceScopeFactory;
            var scope = _serviceScopeFactory.CreateScope();
            uowQuery = scope.ServiceProvider.GetService<IUnityOfWorkQuery>();
        }

        public override void ProcessEvent(string message)
        {
            try
            {
                if (!String.IsNullOrEmpty(message))
                {
                    var cancellationToken = new CancellationToken();
                    var obj = JsonConvert.DeserializeObject<Category>(message);

                    uowQuery.Category.Update(obj);
                    uowQuery.Commit(cancellationToken);
                }
            }
            catch (Exception err)
            {

                throw err;
            }
            
        }
    }
}
