using Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request
{
    public class OrderRequest : BaseRequest
    {
        public int IdCart { get; set; }
        public decimal Total { get; set; }
    }
}
