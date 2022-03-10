using Domain.Common;
using Domain.Model.Base;
using Domain.Model.Response;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request.CartItemRequests
{
    public class GetCartItensRequest : BaseRequest, IRequest<PaginatedList<CartItemModelResponse>>
    {
        public int idCart { get; set; }
        public int? pageNumber  { get; set; }
        public int? pageSize { get; set; }
    }
}
