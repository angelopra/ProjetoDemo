using Business.Base;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
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
    public class PostProduct : ServiceManagerBase, IRequestHandler<PostProductRequest, int>
    {
        private readonly IValidator<ProductRequest> _validator;
        private List<ValidateError> errors;
        private IMessengerBusClient _messenger;

        public PostProduct(IUnityOfWork uow, IValidator<ProductRequest> validator, IMessengerBusClient messenger)
            : base(uow)
        {
            _validator = validator;
            _messenger = messenger;
        }

        public async Task<int> Handle(PostProductRequest request, CancellationToken cancellationToken)
        {
            try
            {
                errors = ValidadeProductRequest(request.Map<ProductRequest>());
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

                //obj.Category = category;

                await _uow.Product.AddAsync(obj);
                await _uow.Commit(cancellationToken);

                _messenger.Publish(EnumGetValue(QueuesEnum.ProductAdd), obj, "ProductKey", "ProductExchange");

                return obj.Id;
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }

        private List<ValidateError> ValidadeProductRequest(ProductRequest request)
        {
            errors = null;
            var validate = _validator.Validate(request);
            if (!validate.IsValid)
            {
                errors = new List<ValidateError>();
                foreach (var failure in validate.Errors)
                {
                    var error = new ValidateError();
                    error.PropertyName = failure.PropertyName;
                    error.Error = failure.ErrorMessage;
                    errors.Add(error);
                }
            }
            return errors;
        }
    }
}
