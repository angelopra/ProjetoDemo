using Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request
{
    public class CartItemUpdateRequest : BaseRequest
    {
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
