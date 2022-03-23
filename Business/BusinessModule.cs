using Business.Base;
using Business.CartBusiness;
using Business.CartBusiness.Get;
using Business.CategoryBusiness;
using Business.CategoryBusiness.Subscriber;
using Business.CustomerBusiness;
using Business.OrderBusiness;
using Business.ProductBusiness;
using Business.ProductBusiness.Subscriber;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Validators;
using FluentValidation;
using Hangfire;
using Hangfire.InMemory;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Business
{
    public static class BusinessModule
    {
        public static void AddBusinessModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<IHelper, BaseBusinessComon>();
            services.AddScoped<ICartBusinessMethods, CartBusinessMethods>();

            services.AddScoped<IOrderComponent, OrderComponent>();

            #region HangFire
            //var inMemory = GlobalConfiguration.Configuration.UseMemoryStorage();
            var inMemoryStorageOptions = new InMemoryStorageOptions()
            {
                DisableJobSerialization = false
            };
            services.AddHangfire(x => x.UseInMemoryStorage(inMemoryStorageOptions));

            services.AddHangfireServer();
            services.AddScoped<ProductAddSubscriber>();
            services.AddScoped<ProductUpdateSubscriber>();
            services.AddScoped<ProductDeleteSubscriber>();
            services.AddScoped<CategoryAddSubscriber>();
            services.AddScoped<CategoryUpdateSubscriber>();
            services.AddScoped<CategoryDeleteSubscriber>();
            #endregion
        }
    }
}
