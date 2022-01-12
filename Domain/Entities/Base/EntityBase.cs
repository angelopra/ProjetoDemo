using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Base
{
    public class EntityBase
    {
        public bool Active { get; set; } = true;

        public DateTime CreateUTC { get; set; } = DateTime.UtcNow;
    }
}
