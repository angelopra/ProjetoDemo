using Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Domain.Entities;

namespace Domain.Model.Request.ProductRequests
{
    public class UpdateProductRequest : BaseRequest, IRequest<Product>
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int IdCategory { get; set; }
    }
}
