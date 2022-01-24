using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Model.Response
{
    public class OrderListResponse
    {
        [JsonPropertyName("IdOrder")]
        public int Id { get; set; }
        public int IdCart { get; set; }
        public decimal Total { get; set; }
        public DateTime CreateUTC { get; set; }
    }
}
