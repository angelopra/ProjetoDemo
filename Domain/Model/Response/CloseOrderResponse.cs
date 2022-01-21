using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Response
{
    public class CloseOrderResponse
    {
        public int IdCart { get; set; }
        public decimal Total { get; set; }
        public DateTime ClosedDate
        {
            get
            {
                return DateTime.UtcNow;
            }
        }
    }
}
