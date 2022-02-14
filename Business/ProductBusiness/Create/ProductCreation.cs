using Business.Base;
using Domain.Entities;
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
    internal class ProductCreation : BaseBusiness<IUnityOfWork>, IRequestHandler<ProductAddRequest, int>
    {
        private readonly IValidator<ProductRequest> _validator;
        private List<ValidateError> errors;
        public ProductCreation(IUnityOfWork uow, IValidator<ProductRequest> validator)
            : base(uow)
        {
            _validator = validator;
        }

        public async Task<int> Handle(ProductAddRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //errors = ValidadeProductRequest(request);
                //if (errors != null)
                //{
                //    throw new Exception();
                //}

                var response = 0;

                var obj = request.Map<Product>();

                //var category = _categoryComponent.GetCategoryById(request.IdCategory);
                var category = _context.categoryRepository.GetCategoryById(request.IdCategory);

                if (category == null)
                {
                    throw new Exception("Category does not exist");
                }
                obj.Category = category;

                response= await _context.productRepository.AddProduct(obj);
                return response;
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
