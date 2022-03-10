using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
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
        private readonly IValidator<ProductRequest> _validator;
        public UpdateProduct(IUnityOfWork uow, IValidator<ProductRequest> validator) : base(uow)
        {
            _validator = validator;
        }

        public async Task<Product> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            try
            {
                errors = ValidadeProductRequest(request.Map<ProductRequest>());
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
                obj.Category = category;

                //if (_uow.Product.Where(p => p.Id == request.ProductId).FirstOrDefault() == null)
                //{
                //    throw new Exception("Product doesn't exist");
                //}

                _uow.Product.Update(obj);
                await _uow.Commit(cancellationToken);

                return obj;
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
