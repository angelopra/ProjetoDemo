using Business.Base;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Messengers.QueueType;
using Domain.Messengers.QueueType.ProductQueues;
using Domain.Model.Request;
using Domain.Model.Request.ProductRequests;
using Domain.Validators;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.ProductBusiness.Update
{
    public class UpdateProduct : ServiceManagerBase, IRequestHandler<UpdateProductRequest, Product>
    {
        private List<ValidateError> errors;
        private readonly ProductUpdateQueue _productUpdQueue;
        private IMessengerBusClient _messenger;

        public UpdateProduct(
            IUnityOfWork uow
            ,IMessengerBusClient messenger
            ,ProductUpdateQueue productUpdQueue) : base(uow)
        {
            _messenger = messenger;
            _productUpdQueue = productUpdQueue;
        }

        public async Task<Product> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            try
            {
                errors = ValidateObj<ProductRequest>(request);
                if (errors != null)
                {
                    throw new Exception();
                }

                var obj = request.Map<Product>();
                obj.Id = request.ProductId;

                var category = _uow.Category.Where(c => c.Id == request.IdCategory).FirstOrDefault();
                if (category == null)
                {
                    throw new Exception("Category does not exist");
                }

                _uow.Product.Update(obj);
                await _uow.Commit(cancellationToken);

                _messenger.Publish(_productUpdQueue.QueueName, obj, _productUpdQueue.RoutingKey, _productUpdQueue.Exchange);

                return obj;
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }
    }
}
