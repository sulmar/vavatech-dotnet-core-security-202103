using AuthenticationHandlers;
using Bogus;
using Fakers;
using FakeServices;
using IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApi
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
            services.AddSingleton<IOrderService, FakeOrderService>();
            services.AddSingleton<Faker<Order>, OrderFaker>();
            services.AddSingleton<Faker<Customer>, CustomerFaker>();
            services.AddSingleton<IServices.IAuthorizationService, CustomerAuthorizationService>();
            services.AddSingleton<ICustomerService, FakeCustomerService>();

            services.AddScoped<IClaimsTransformation, CustomerClaimsTransformation>();

            services.AddAuthentication(defaultScheme: "Basic")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Creator", policy =>
                {
                   policy.RequireAuthenticatedUser();
                   policy.RequireRole("Creator");
                });

                options.AddPolicy("Adult", policy =>
                {
                    policy.RequireAge(26);
                });
            });

            services.AddScoped<IAuthorizationHandler, MinimumAgeHandler>();
            services.AddScoped<IAuthorizationHandler, OrderAuthorizationHandler>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();  // uwaga na kolejnoœæ!         
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
