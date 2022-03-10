using Domain.Model.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Domain.Model.Request.CartRequests
{
    public class RemoveCartRequest : IRequest<CartResponse>
    {
        public int Id { get; set; }
    }
}
