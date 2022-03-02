using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Model.Request.CartItemRequests;
using Domain.Model.Response;
using Domain.Validators;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.CartBusiness.Post
{
    public class PostCartItem : ServiceManagerBase, IRequestHandler<PostCartItemRequest, CartItemModelResponse>
    {
        private readonly IValidator<CartItemRequest> _validator;
        private ICartBusinessMethods _cbm;
        private List<ValidateError> errors;
        public PostCartItem(IUnityOfWork uow, IValidator<CartItemRequest> validator, ICartBusinessMethods cbm) : base(uow)
        {
            _validator = validator;
            _cbm = cbm;
        }

        public async Task<CartItemModelResponse> Handle(PostCartItemRequest request, CancellationToken cancellationToken)
        {
            try
            {
                errors = ValidadeCartItemRequest(request.Map<CartItemRequest>());
                if (errors != null)
                {
                    throw new Exception();
                }

                CartItem obj;

                // Updating correspondent cart Total value
                var cart = _cbm.GetCartById(request.IdCart);

                cart.Total += request.UnitPrice * request.Quantity;

                _uow.Cart.Update(cart);

                if (_cbm.CartItemExists(request.IdCart, request.IdProduct))
                {
                    obj = IncreaseCartItem(request);
                    _uow.CartItem.Update(obj);
                }
                else
                {
                    obj = request.Map<CartItem>();
                    _uow.CartItem.Add(obj);
                }
                await _uow.Commit(cancellationToken);
                return obj.Map<CartItemModelResponse>();
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }
        private CartItem IncreaseCartItem(PostCartItemRequest cartItem)
        {
            try
            {
                var item = _cbm.GetCartItem(cartItem.IdCart, cartItem.IdProduct);
                item.Quantity += cartItem.Quantity;

                return item;
            }
            catch (Exception err)
            {

                throw err;
            }
        }
        private List<ValidateError> ValidadeCartItemRequest(CartItemRequest request)
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
