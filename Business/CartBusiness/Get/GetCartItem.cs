using Business.Base;
using Domain.Interfaces;
using Domain.Model.Request.CartItemRequests;
using Domain.Model.Response;
using Domain.Validators;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.CartBusiness.Get
{
    public class GetCartItem : ServiceManagerBase, IRequestHandler<GetCartItemRequest, CartItemModelResponse>
    {
        private List<ValidateError> errors;
        public GetCartItem(IUnityOfWork uow) : base(uow)
        {
        }

        public async Task<CartItemModelResponse> Handle(GetCartItemRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = _uow.CartItem.Where(c => c.IdCart == request.idCart && c.IdProduct == request.idProduct).Include(n => n.Cart).FirstOrDefault();
                if (response == null)
                {
                    throw new Exception("cart or product doesn't exist");
                }
                return response.Map<CartItemModelResponse>();
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }
    }
}
