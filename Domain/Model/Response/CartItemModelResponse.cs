using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Model.Response
{
    public class CartItemModelResponse
    {
        [JsonPropertyName("idCartItem")]
        public int Id { get; set; }
        public int IdCart { get; set; }
        public int IdProduct { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalValue
        {
            get
            {
                return (UnitPrice * Quantity);
            }
        }
    }
}
