using DataBase.DataBaseQuery;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseQuery
{
    public static class DataBaseQueryModuleDependency
    {
        public static void AddDataBaseQueryModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<DataBaseQueryContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServerQuery"));
            });

            services.AddScoped<IUnityOfWorkQuery, DataBaseQueryContext>();
        }
    }
}
