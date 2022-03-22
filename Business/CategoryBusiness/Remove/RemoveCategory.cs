using Business.Base;
using Domain.Interfaces;
using Domain.Messengers.QueueType.CategoryQueues;
using Domain.Model.Request.CategoryRequests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.CategoryBusiness.Remove
{
    public class RemoveCategory : ServiceManagerBase, IRequestHandler<RemoveCategoryRequest, int>
    {
        private readonly IMessengerBusClient _messenger;
        private readonly CategoryDeleteQueue _categoryDeleteQueue;

        public RemoveCategory(IUnityOfWork uow, IMessengerBusClient messenger, CategoryDeleteQueue categoryDeleteQueue) : base(uow)
        {
            _messenger = messenger;
            _categoryDeleteQueue = categoryDeleteQueue;
        }

        public async Task<int> Handle(RemoveCategoryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var category = _uow.Category.Where(c => c.Id == request.Id).FirstOrDefault();
                if (category == null)
                {
                    throw new Exception("Category doesn't exist");
                }

                _uow.Category.Remove(category);
                await _uow.Commit(cancellationToken);
                _messenger.Publish(_categoryDeleteQueue.QueueName, category, _categoryDeleteQueue.RoutingKey, _categoryDeleteQueue.Exchange);

                return request.Id;
            }
            catch (Exception err)
            {
                MapperException(err);
                throw;
            }
        }
    }
}
