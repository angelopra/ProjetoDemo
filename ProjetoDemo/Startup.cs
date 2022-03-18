using DataBase;
using Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProjetoDemo.Filter;
using Domain.Entities.Security;
using Business.AuthenticationBusiness;
using Microsoft.Extensions.Options;
using Business;
using ProjetoDemo.Messenger;
using DataBaseQuery;
using ProjetoDemo.BackgroundService;

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
            services.AddDataBaseQueryModule(Configuration);
            services.AddBusinessModule(Configuration);
            services.AddMessagerModule();
            services.AddHangfireModule(Configuration);

            #region Autentication
            // Configurando a dependência para a classe de validação
            // de credenciais e geração de tokens
            services.AddScoped<IAuthenticationComponent, AuthenticationComponent>();
            services.AddScoped<IdentityInitializer>();

            var tokenConfigurations = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                Configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            // Aciona a extensão que irá configurar o uso de
            // autenticação e autorização via tokens
            services.AddJwtSecurity(tokenConfigurations);

            // Configura a dependência da classe que cria usuários
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
