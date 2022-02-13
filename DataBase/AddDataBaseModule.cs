using DataBase.Context;
using DataBase.Repository;
using DataBase.Repository.Base;
using Domain.Entities.Security;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace DataBase
{
    public static class DataBaseModuleDependency
    {
        public static void AddDataBaseModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<CoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnityOfWork, UnitOfWork>();
            //services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();

            #region Autentication
            // Ativando a utilização do ASP.NET Identity, a fim de
            // permitir a recuperação de seus objetos via injeção de
            // dependências
            services.AddIdentity<AuthorizationUserDB, IdentityRole>()
                .AddEntityFrameworkStores<AuthenticationContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<AuthenticationContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion
        }
    }
}
