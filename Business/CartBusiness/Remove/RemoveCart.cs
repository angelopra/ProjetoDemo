using Business.Base;
using Domain.Interfaces;
using Domain.Model.Request.CartRequests;
using Domain.Model.Response;
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
    public class RemoveCart : ServiceManagerBase, IRequestHandler<RemoveCartRequest, CartResponse>
    {
        private List<ValidateError> errors;
        public RemoveCart(IUnityOfWork uow) : base(uow)
        {
        }

        public async Task<CartResponse> Handle(RemoveCartRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var cart = _uow.Cart.Where(c => c.Id == request.Id).FirstOrDefault();
                if (cart != null)
                {
                    _uow.Cart.Remove(cart);
                    await _uow.Commit(cancellationToken);
                    return cart.Map<CartResponse>();
                }
                else
                {
                    throw new Exception("Cart not found");
                }

            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }
    }
}
