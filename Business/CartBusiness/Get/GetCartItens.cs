using Business.Base;
using Business.CartBusiness.StaticMethods;
using Domain.Interfaces;
using Domain.Model.Request.CartItemRequests;
using Domain.Model.Response;
using Domain.Validators;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.CartBusiness.Get
{
    public class GetCartItens : ServiceManagerBase, IRequestHandler<GetCartItensRequest, IEnumerable>
    {
        private List<ValidateError> errors;
        public GetCartItens(IUnityOfWork uow) : base(uow)
        {
        }

        public async Task<IEnumerable> Handle(GetCartItensRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (CartExists(request.idCart))
                {
                    var query = from c in _uow.CartItem
                                where c.IdCart == request.idCart
                                select c;

                    var paginatedItemList = query.Paginate(request.pageNumber, request.pageSize).Map<List<CartItemModelResponse>>();

                    return paginatedItemList;
                }
                else
                {
                    throw new Exception("Cart doesn't exists");
                }
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }

        private bool CartExists(int id)
        {
            try
            {
                var item = _uow.Cart.Where(c => id == c.Id).FirstOrDefault();
                if (item == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception err)
            {
                throw err;

            }
        }
    }
}
