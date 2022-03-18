﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Hangfire.SqlServer;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Domain.Interfaces;

namespace ProjetoDemo.BackGroundService
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

            services.AddSingleton<IProductSubscriber, ProductSubscriber>();
        }

    }
}
