using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Microsoft.Extensions.Configuration;

namespace ProjectDemo.Hangfire
{
    public static class HangfireModule
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
