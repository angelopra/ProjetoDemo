using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductSubscriber
    {
        public Task ExecuteAsync();
        public void ProcessEvent(string message);
    }
}
