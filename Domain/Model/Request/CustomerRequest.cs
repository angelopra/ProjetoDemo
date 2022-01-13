using Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request
{
    public class CustomerRequest : BaseRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
