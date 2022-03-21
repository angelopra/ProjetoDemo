using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProjetoDemo.BackgroundService
{
    public static class BackGroundServiceModules
    {
        public static void AddHangfireModule(this IServiceCollection services, IConfiguration configuration)
        {
            GlobalConfiguration.Configuration.UseMemoryStorage();
        }
    }
}
