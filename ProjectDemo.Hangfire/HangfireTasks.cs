using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDemo.Hangfire
{
    public static class HangfireTasks
    {
        public static async Task Teste(IProductSubscriber subscriber)
        {
            await subscriber.ExecuteAsync();
        }
    }
}
