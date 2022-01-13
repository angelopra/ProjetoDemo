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
    public class Cart : EntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdCustomer { get; set; }

        [ForeignKey(nameof(IdCustomer))]
        public Customer Customer { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        public bool IsClosed { get; set; }
    }
}
