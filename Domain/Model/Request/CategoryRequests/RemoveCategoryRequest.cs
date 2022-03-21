using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request.CategoryRequests
{
    public class RemoveCategoryRequest : IRequest<int>
    {
        public int Id { get; set; }

        public RemoveCategoryRequest(int id)
        {
            Id = id;
        }
    }
}
