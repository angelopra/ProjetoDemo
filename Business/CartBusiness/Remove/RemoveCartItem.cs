using Business.Base;
using Business.CartBusiness.StaticMethods;
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
        public RemoveCartItem(IUnityOfWork uow) : base(uow)
        {
        }

        public async Task<int> Handle(RemoveCartItemRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var cartItem = CartBusinessStaticMethods.GetCartItem(request.idCart, request.idProduct, _uow);

                // Updating correspondent cart Total value
                var cart = CartBusinessStaticMethods.GetCartById(request.idCart, _uow);
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
