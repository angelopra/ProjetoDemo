using Domain.Common;
using Domain.Model.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request.CategoryRequests
{
    public class GetAllCategoriesRequest : IRequest<PaginatedList<CategoryResponse>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public GetAllCategoriesRequest(int? pageNumber, int? pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
