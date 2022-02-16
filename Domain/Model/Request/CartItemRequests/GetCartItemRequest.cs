using Domain.Model.Base;
using Domain.Model.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request.CartItemRequests
{
    public class GetCartItemRequest : BaseRequest, IRequest<CartItemModelResponse>
    {
        public int idCart { get; set; }
        public int idProduct { get; set; }
    }
}
