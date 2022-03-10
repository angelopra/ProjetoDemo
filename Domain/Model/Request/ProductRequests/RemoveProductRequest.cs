using Domain.Model.Base;
using MediatR;

namespace Domain.Model.Request.ProductRequests
{
    public class RemoveProductRequest : BaseRequest, IRequest<int>
    {
        public int id { get; set; }
    }
}
