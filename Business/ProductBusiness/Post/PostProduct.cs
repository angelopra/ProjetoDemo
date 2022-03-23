using Business.Base;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Messengers.QueueType.ProductQueues;
using Domain.Model.Request;
using Domain.Validators;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.ProductBusiness.Create
{
    public class PostProduct : ServiceManagerBase, IRequestHandler<PostProductRequest, Product>
    {
        private List<ValidateError> errors;
        private IMessengerBusClient _messenger;
        private readonly ProductAddQueue _productAddQueue;

        public PostProduct(
            IUnityOfWork uow
            ,IMessengerBusClient messenger
            ,ProductAddQueue productAddQueue)
            : base(uow)
        {
            _messenger = messenger;
            _productAddQueue = productAddQueue;
        }

        public async Task<Product> Handle(PostProductRequest request, CancellationToken cancellationToken)
        {
            try
            {
                errors = ValidateObj<ProductRequest>(request);
                if (errors != null)
                {
                    throw new Exception();
                }

                var obj = request.Map<Product>();

                var category = _uow.Category.Where(c => c.Id == request.IdCategory).FirstOrDefault();

                if (category == null)
                {
                    throw new Exception("Category doesn't exist");
                }

                obj.Category = category;

                await _uow.Product.AddAsync(obj);
                await _uow.Commit(cancellationToken);

                _messenger.Publish(_productAddQueue.QueueName, obj, _productAddQueue.RoutingKey, _productAddQueue.Exchange);

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
