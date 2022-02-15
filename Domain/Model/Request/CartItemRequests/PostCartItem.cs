using Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Domain.Model.Response;

namespace Domain.Model.Request.CartItemRequests
{
    public class PostCartItem : BaseRequest, IRequest<CartItemModelResponse>
    {
        public int IdCart { get; set; }
        public int IdProduct { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
