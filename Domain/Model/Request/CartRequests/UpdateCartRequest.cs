using Domain.Model.Base;
using Domain.Model.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request.CartRequests
{
    public class UpdateCartRequest : BaseRequest, IRequest<CartResponse>
    {
        public int IdCart { get; set; }
        public int IdCustomer { get; set; }
        public bool IsClosed { get; set; }
    }
}
