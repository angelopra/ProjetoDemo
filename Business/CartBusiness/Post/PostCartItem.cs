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
        private ICartBusinessMethods _cartBusinessMethods;
        private List<ValidateError> errors;
        public PostCartItem(IUnityOfWork uow
            ,ICartBusinessMethods cartBusinessMethods) : base(uow)
        {
            _cartBusinessMethods = cartBusinessMethods;
        }

        public async Task<CartItemModelResponse> Handle(PostCartItemRequest request, CancellationToken cancellationToken)
        {
            try
            {
                errors = ValidateObj<CartItemRequest>(request);
                if (errors != null)
                {
                    throw new Exception();
                }

                CartItem obj;

                // Updating correspondent cart Total value
                var cart = _cartBusinessMethods.GetCart(request.IdCart);

                cart.Total += request.UnitPrice * request.Quantity;

                _uow.Cart.Update(cart);

                if (_cartBusinessMethods.CartItemExists(request.IdCart, request.IdProduct))
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
                var item = _cartBusinessMethods.GetCartItem(cartItem.IdCart, cartItem.IdProduct);
                item.Quantity += cartItem.Quantity;

                return item;
            }
            catch (Exception err)
            {

                throw err;
            }
        }
    }
}
