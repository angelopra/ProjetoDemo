using Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Domain.Model.Request.CartRequests
{
    public class PostCartRequest : BaseRequest, IRequest<int>
    {
        public int IdCustomer { get; set; }
        public bool IsClosed { get; set; }
    }
}
