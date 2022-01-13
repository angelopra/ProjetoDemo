using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CartItem : EntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdCart { get; set; }

        [ForeignKey(nameof(IdCart))]
        public Cart Cart { get; set; }

        [Required]
        public int IdProduct { get; set; }

        [ForeignKey(nameof(IdProduct))]
        public Product Product { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
