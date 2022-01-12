using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request
{
    public class CategoryRequest
    {
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
