using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Business.Base;
using Domain.Interfaces;
using Domain.Model.Request.CartRequests;
using Domain.Model.Response;
using Domain.Validators;
using MediatR;

namespace Business.CartBusiness.Get
{
    public class GetCart : ServiceManagerBase, IRequestHandler<GetCartRequest, CartResponse>
    {
        private List<ValidateError> errors;
        public GetCart(IUnityOfWork uow) : base(uow)
        {
        }

        public async Task<CartResponse> Handle(GetCartRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = _uow.Cart.Where(c => c.Id == request.id).FirstOrDefault();
                if(response == null)
                {
                    throw new Exception("Cart not found");
                }

                var mappedResponse = response.Map<CartResponse>();
                return mappedResponse;
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }
    }
}
