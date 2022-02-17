using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Model.Request.CartRequests;
using Domain.Model.Response;
using Domain.Validators;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.CartBusiness.Update
{
    public class UpdateCart : ServiceManagerBase, IRequestHandler<UpdateCartRequest, CartResponse>
    {
        private List<ValidateError> errors;
        private readonly IValidator<CartRequest> _validator;
        public UpdateCart(IUnityOfWork uow, IValidator<CartRequest> validator) : base(uow)
        {
            _validator = validator;
        }

        public async Task<CartResponse> Handle(UpdateCartRequest request, CancellationToken cancellationToken)
        {
            try
            {
                errors = ValidadeCartRequest(request.Map<CartRequest>());
                if (errors != null)
                {
                    throw new Exception();
                }

                var cart = GetCart(request.IdCart);
                cart.Active = request.Active;
                cart.IdCustomer = request.IdCustomer;
                cart.IsClosed = request.IsClosed;

                _uow.Cart.Update(cart);
                await _uow.Commit(cancellationToken);

                var responseMapped = cart.Map<CartResponse>();
                return responseMapped;
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }

        private Cart GetCart(int id)
        {
            try
            {
                var cart = _uow.Cart.Where(c => c.Id == id).FirstOrDefault();
                if (cart == null)
                {
                    throw new Exception("Cart not found");
                }
                return cart;
            }
            catch (Exception err)
            {
                throw err;
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
