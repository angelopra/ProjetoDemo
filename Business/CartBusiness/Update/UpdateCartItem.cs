﻿using Business.Base;
using Business.CartBusiness.Get;
using Business.CartBusiness.StaticMethods;
using Domain.Interfaces;
using Domain.Model.Request;
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
    public class UpdateCartItem : ServiceManagerBase, IRequestHandler<CartItemUpdateRequest, CartItemModelResponse>
    {
        private List<ValidateError> updateErrors;
        IValidator<CartItemUpdateRequest> _updateValidator;
        public UpdateCartItem(IUnityOfWork uow, IValidator<CartItemUpdateRequest> updateValidator) : base(uow)
        {
            _updateValidator = updateValidator;
        }

        public async Task<CartItemModelResponse> Handle(CartItemUpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                updateErrors = ValidadeCartItemUpdateRequest(request);
                if (updateErrors != null)
                {
                    throw new Exception();
                }
                var cartItem = CartBusinessStaticMethods.GetCartItem(request.IdCart, request.IdProduct, _uow);

                // Updating correspondent cart Total value
                var cart = CartBusinessStaticMethods.GetCartById(request.IdCart, _uow);

                if (cart.IsClosed)
                {
                    throw new Exception("Cart is closed, cannot update");
                }
                decimal initialTotal = cartItem.Quantity * cartItem.UnitPrice;
                decimal finalTotal = request.Quantity * request.UnitPrice;
                decimal difference = finalTotal - initialTotal;
                cart.Total += difference;
                _uow.Cart.Update(cart);

                cartItem.Quantity = request.Quantity;
                cartItem.UnitPrice = request.UnitPrice;

                _uow.CartItem.Update(cartItem);
                await _uow.Commit(cancellationToken);

                CartItemModelResponse response = cartItem.Map<CartItemModelResponse>();

                return response;
            }
            catch (Exception err)
            {
                MapperException(err, updateErrors);
                throw;
            }
        }

        private List<ValidateError> ValidadeCartItemUpdateRequest(CartItemUpdateRequest request)
        {
            updateErrors = null;
            var validate = _updateValidator.Validate(request);
            if (!validate.IsValid)
            {
                updateErrors = new List<ValidateError>();
                foreach (var failure in validate.Errors)
                {
                    var error = new ValidateError();
                    error.PropertyName = failure.PropertyName;
                    error.Error = failure.ErrorMessage;
                    updateErrors.Add(error);
                }
            }
            return updateErrors;
        }
    }
}
