using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDemo.BackgroundService
{
    public static class BackGroundServiceModules
    {
        public static void AddHangfireModule(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("HangfireConnection");

            // Add Hangfire services.
            services.AddHangfire(configuration =>
            {
                configuration.UseSqlServerStorage(connectionString);
            });
            // Add the processing server as IHostedService
            services.AddHangfireServer();
        }
    }
}
