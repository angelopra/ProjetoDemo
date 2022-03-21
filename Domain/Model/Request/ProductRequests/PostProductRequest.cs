using Domain.Entities;
using Domain.Model.Base;
using MediatR;

namespace Domain.Model.Request
{
    public class PostProductRequest : BaseRequest, IRequest<Product>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int IdCategory { get; set; }

    }
}
