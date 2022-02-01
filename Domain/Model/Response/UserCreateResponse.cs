using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Response
{
    public class UserCreateResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
