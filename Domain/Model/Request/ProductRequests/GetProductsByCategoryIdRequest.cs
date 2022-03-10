using Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Domain.Model.Response;
using Domain.Common;

namespace Domain.Model.Request.ProductRequests
{
    public class GetProductsByCategoryIdRequest : BaseRequest, IRequest<PaginatedList<ProductListResponse>>
    {
        public int categoryId { get; set; } 
        public int? pageNumber { get; set; }
        public int? pageSize { get; set; }  
    }
}
