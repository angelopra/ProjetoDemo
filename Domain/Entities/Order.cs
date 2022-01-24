using Domain.Entities.Base;
using Domain.Model.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdCart { get; set; }

        [ForeignKey(nameof(IdCart))]
        public Cart Cart { get; set; }

        [Required]
        public decimal Subtotal { get; set; }

        [Required]
        public decimal Discounts { get; set; } = 0m;

        [Required]
        public decimal Total { get; set; }

        public DateTime CreateUTC { get; set; } = DateTime.UtcNow;
    }
}
