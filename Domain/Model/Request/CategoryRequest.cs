using Domain.Model.Base;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request
{
    public class CategoryRequest : BaseRequest
    {
        public string Name { get; set; }

    }
}
