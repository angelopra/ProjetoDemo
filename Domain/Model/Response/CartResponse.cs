using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Response
{
    public class CartResponse : EntityBase
    {
        public int Id { get; set; }
        public int IdCustomer { get; set; }
        public decimal Total { get; set; }
        public bool IsClosed { get; set; }
    }
}
