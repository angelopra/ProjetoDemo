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
        private ICartBusinessMethods _cbm;
        public RemoveCartItem(IUnityOfWork uow, ICartBusinessMethods cbm) : base(uow)
        {
            _cbm = cbm;
        }

        public async Task<int> Handle(RemoveCartItemRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var cartItem = _cbm.GetCartItem(request.idCart, request.idProduct);

                // Updating correspondent cart Total value
                var cart = _cbm.GetCartById(request.idCart);
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
