using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Category : EntityBase
    {
        [Key]
        [JsonPropertyName("IdCategory")]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
