using Business.Base;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Messengers.QueueType.ProductQueues;
using Domain.Model.Request.ProductRequests;
using Domain.Validators;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.ProductBusiness.Remove
{
    public class RemoveProduct : ServiceManagerBase, IRequestHandler<RemoveProductRequest, int>
    {
        private List<ValidateError> errors;
        private IMessengerBusClient _messenger;
        private ProductRemoveQueue _productRemoveQueue;

        public RemoveProduct(IUnityOfWork uow, IMessengerBusClient messenger, ProductRemoveQueue productRemoveQueue) : base(uow)
        {
            _messenger = messenger;
            _productRemoveQueue = productRemoveQueue;

        }

        public async Task<int> Handle(RemoveProductRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var product = _uow.Product.Where(c => c.Id == request.Id).FirstOrDefault();
                if (product == null)
                {
                    throw new Exception("Product doesn't exist");
                }

                _uow.Product.Remove(product);
                await _uow.Commit(cancellationToken);

                _messenger.Publish(EnumGetValue(QueuesEnum.ProductAdd), product, _productRemoveQueue.RoutingKey, _productRemoveQueue.Exchange);

                return request.Id;
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }
    }
}
