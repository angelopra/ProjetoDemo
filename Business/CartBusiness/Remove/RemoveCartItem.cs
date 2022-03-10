using Business.Base;
using Domain.Interfaces;
using Domain.Model.Request.CartItemRequests;
using Domain.Validators;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.CartBusiness.Remove
{
    public class RemoveCartItem : ServiceManagerBase, IRequestHandler<RemoveCartItemRequest, int>
    {
        private List<ValidateError> errors;

        private ICartBusinessMethods _cartBusinessMethods;
        public RemoveCartItem(IUnityOfWork uow, ICartBusinessMethods cartBusinessMethods) : base(uow)
        {
            _cartBusinessMethods = cartBusinessMethods;
        }

        public async Task<int> Handle(RemoveCartItemRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var cartItem = _cartBusinessMethods.GetCartItem(request.idCart, request.idProduct);

                // Updating correspondent cart Total value
                var cart = _cartBusinessMethods.GetCart(request.idCart);
                cart.Total -= cartItem.UnitPrice * cartItem.Quantity;
                _uow.Cart.Update(cart);

                _uow.CartItem.Remove(cartItem);
                await _uow.Commit(cancellationToken);

                return 0;
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }
    }
}
