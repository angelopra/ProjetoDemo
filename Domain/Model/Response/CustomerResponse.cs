using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Model.Response
{
    public class CustomerResponse
    {
        [JsonPropertyName("IdCustomer")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? IdCart { get; set; }
    }
}
