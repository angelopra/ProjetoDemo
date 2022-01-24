using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Model.Response
{
    public class OrderResponse
    {
        [JsonPropertyName("IdOrder")]
        public int Id { get; set; }
        public int IdCart { get; set; }
        public List<CartItemModelResponse> Items { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Discounts { get; set; }
        public decimal Total { get; set; }
        public DateTime CreateUTC { get; set; }
    }
}
