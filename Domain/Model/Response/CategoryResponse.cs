using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Model.Response
{
    public class CategoryResponse
    {
        [JsonPropertyName("IdCategory")]
        public int Id { get; set; }
    }
}
