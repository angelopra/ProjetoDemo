using Domain.Entities;
using Domain.Model.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request.CustomerRequests
{
    public class GetCustomerByIdRequest : BaseRequest, IRequest<Customer>
    {
        public int id { get; set; }
    }
}
