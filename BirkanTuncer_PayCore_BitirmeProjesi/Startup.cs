using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using BirkanTuncer_PayCore_BitirmeProjesi.StartUpExtension;
using BirkanTuncer_PayCore_BitirmeProjesi.Base;
using Hangfire;
using Hangfire.PostgreSql;
using System;

namespace BirkanTuncer_PayCore_BitirmeProjesi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static JwtConfig JwtConfig { get; private set; }


        public void ConfigureServices(IServiceCollection services)
        {
            var connStr = Configuration.GetConnectionString("PostgreSqlConnection");

            services.AddNHibernatePosgreSql(connStr);


            // Configure JWT Bearer
            JwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

            // service
            services.AddServices();
            services.AddJwtBearerAuthentication();
            services.AddCustomizeSwagger();
            ////services.AddRedisDependencyInjection(Configuration);

            //hangfire 
            services.AddHangfire(configuration => configuration
               .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
               .UseSimpleAssemblyNameTypeSerializer()
               .UseRecommendedSerializerSettings()
               .UsePostgreSqlStorage(Configuration.GetConnectionString("PostgreSqlConnection"), new PostgreSqlStorageOptions
               {
                   TransactionSynchronisationTimeout = TimeSpan.FromMinutes(5),
                   InvisibilityTimeout = TimeSpan.FromMinutes(5),
                   QueuePollInterval = TimeSpan.FromMinutes(5),
               }));


            services.AddHangfireServer();



            services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BitirmeProjesi_PayCore", Version = "v1" });
            //});
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BitirmeProjesi_PayCore v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseHangfireDashboard();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
