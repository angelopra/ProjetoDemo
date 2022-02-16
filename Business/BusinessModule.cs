﻿using Business.CartBusiness;
using Business.CategoryBusiness;
using Business.CustomerBusiness;
using Business.OrderBusiness;
using Business.ProductBusiness;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Validators;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Business
{
    public static class BusinessModule
    {
        public static void AddBusinessModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            // Entities DI
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
        }
    }
}