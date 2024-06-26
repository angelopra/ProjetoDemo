using Business.CategoryBusiness;
using Business.CustomerBusiness;
using Business.ProductBusiness;
using Business.CartBusiness;
using Business.OrderBusiness;
using DataBase;
using Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Validators;
using Domain.Model.Request;
using FluentValidation;
using ProjetoDemo.Filter;
using Domain.Entities.Security;
using Business.AuthenticationBusiness;
using Microsoft.Extensions.Options;

namespace ProjetoDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataBaseModule(Configuration);

            // Entities DI
            services.AddScoped<IProductComponent, ProductComponent>();
            services.AddScoped<ICategoryComponent, CategoryComponent>();
            services.AddScoped<ICustomerComponent, CustomerComponent>();
            services.AddScoped<ICartComponent, CartComponent>();
            services.AddScoped<ICartItemComponent, CartItemComponent>();
            services.AddScoped<IOrderComponent, OrderComponent>();

            // Validators DI
            services.AddScoped<IValidator<CartItemUpdateRequest>, CartItemUpdateValidator>();
            services.AddScoped<IValidator<CartItemRequest>, CartItemValidator>();
            services.AddScoped<IValidator<CartRequest>, CartValidator>();
            services.AddScoped<IValidator<CategoryRequest>, CategoryValidator>();
            services.AddScoped<IValidator<CustomerRequest>, CustomerValidator>();
            services.AddScoped<IValidator<CustomerLoginRequest>, CustomerLoginValidator>();
            services.AddScoped<IValidator<ProductRequest>, ProductValidator>();
            services.AddScoped<IValidator<OrderRequest>, OrderValidator>();

            #region Autentication
            // Configurando a depend�ncia para a classe de valida��o
            // de credenciais e gera��o de tokens
            services.AddScoped<IAuthenticationComponent, AuthenticationComponent>();
            services.AddScoped<IdentityInitializer>();

            var tokenConfigurations = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                Configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            // Aciona a extens�o que ir� configurar o uso de
            // autentica��o e autoriza��o via tokens
            services.AddJwtSecurity(tokenConfigurations);

            // Configura a depend�ncia da classe que cria usu�rios
            // para testes da API
            //services.AddTransient<IdentityInitializer>();
            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.DocumentFilter<SwaggerTagFilter>();
                c.SwaggerDoc(
                    "v1"
                    , new OpenApiInfo
                    {
                        Title = "ProjetoDemo",
                        Version = "v1",
                    }
                );
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
                        },
                        new string[] {}
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IdentityInitializer identityInitializer)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjetoDemo v1"));
            }

            app.UseCors(builder => builder.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());
            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            identityInitializer.Initialize();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
