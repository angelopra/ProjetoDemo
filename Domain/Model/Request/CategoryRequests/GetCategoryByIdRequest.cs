using Domain.Model.Base;
using Domain.Model.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request.CategoryRequests
{
    public class GetCategoryByIdRequest : BaseRequest, IRequest<CategoryResponse>
    {
        public int Id { get; set; }

        public GetCategoryByIdRequest(int id)
        {
            Id = id;
        }
    }
}
