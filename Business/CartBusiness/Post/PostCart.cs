using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Model.Request.CartRequests;
using Domain.Validators;
using FluentValidation;
using MediatR;

namespace Business.CartBusiness.Post
{
    public class PostCart : ServiceManagerBase, IRequestHandler<PostCartRequest, int>
    {
        private readonly IValidator<CartRequest> _validator;
        private List<ValidateError> errors;
        public PostCart(IUnityOfWork uow, IValidator<CartRequest> validator) : base(uow)
        {
            _validator = validator;
        }

        public async Task<int> Handle(PostCartRequest request, CancellationToken cancellationToken)
        {
            try
            {
                errors = ValidadeCartRequest(request.Map<CartRequest>());
                if (errors != null)
                {
                    throw new Exception();
                }
                var obj = request.Map<Cart>();

                _uow.Cart.Add(obj);
                await _uow.Commit(cancellationToken);

                return obj.Id;
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }
        private List<ValidateError> ValidadeCartRequest(CartRequest request)
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
