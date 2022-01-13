using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request
{
    public class CartRequest
    {
        public int IdCustomer { get; set; }
        public bool IsClosed { get; set; }
        public bool Active { get; set; }
    }
}
