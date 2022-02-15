using Domain.Model.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Model.Request.ProductRequests
{
    public class GetProductByIdRequest : BaseRequest, IRequest<Product>
    {
        public int id { get; set; }
    }
}
