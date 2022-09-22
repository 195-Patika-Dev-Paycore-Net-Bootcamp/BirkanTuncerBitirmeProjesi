using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BirkanTuncer_PayCore_BitirmeProjesi.Mapper;
using BirkanTuncer_PayCore_BitirmeProjesi.Service;
using StackExchange.Redis;
using System;

namespace BirkanTuncer_PayCore_BitirmeProjesi.StartUpExtension
{
    public static class ExtensionService
    {
        

        public static void AddServices(this IServiceCollection services)
        {
            // services 
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IOfferService, OfferService>();
            services.AddScoped<IUserOperationService, UserOperationService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IEmailRabbitMQService, EmailRabbitMQService>();


            // mapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            services.AddSingleton(mapperConfig.CreateMapper());
        }
    }
}
