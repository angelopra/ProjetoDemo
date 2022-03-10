using Domain.Model.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request.CartRequests
{
    public class GetCartRequest : IRequest<CartResponse>
    {
        public int id { get; set; }
    }
}
