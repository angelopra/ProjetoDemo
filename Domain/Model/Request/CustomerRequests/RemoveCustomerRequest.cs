using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request.CustomerRequests
{
    public class RemoveCustomerRequest : IRequest<int>
    {
        public int Id { get; set; }

        public RemoveCustomerRequest(int id)
        {
            Id = id;
        }
    }
}
