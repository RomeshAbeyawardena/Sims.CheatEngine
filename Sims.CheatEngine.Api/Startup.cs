using System;
using System.Globalization;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sims.CheatEngine.Broker;
using Sims.CheatEngine.Contracts;
using Sims.CheatEngine.Domains;
using Swashbuckle.AspNetCore.Swagger;
using WebToolkit.Common;
using WebToolkit.Common.Extensions;

namespace Sims.CheatEngine.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IConnectionStrings, ConnectionStrings>()
                .AddSingleton<IApplicationSettings, ApplicationSettings>()
                .RegisterProviders()
                .RegisterServicesFromAssemblies<ServiceBroker>();

            var applicationSettings = services.GetRequiredService<IApplicationSettings>();

            var serializer = JsonSerializer.CreateDefault();
            serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            services
                .AddSingleton(
                    DefaultJSonSettings.Create(settings =>
                    {
                        settings.LoadSettings = new JsonLoadSettings();
                        settings.Serializer = serializer;
                    }))
                .AddSingleton<IFormatProvider>(new CultureInfo(applicationSettings.CultureName))
                .AddDistributedMemoryCache(setup => setup.SizeLimit = applicationSettings.DistributedMemoryCacheSizeLimitInBytes)
                .AddAutoMapper(
                    Assembly.GetAssembly(typeof(Constants)),
                    Assembly.GetAssembly(typeof(Startup)))
                .AddMvc();

            services.AddSwaggerGen(gen =>
            {
                gen.DescribeAllEnumsAsStrings();
                gen.SwaggerDoc("v1", new Info
                {
                    Title = "Budget Tracker",
                    Version = "v1",
                    License = new License
                    {
                        Name = "MIT",
                        Url = "https://opensource.org/licenses/MIT"
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder
                .UseMvc(BuildRoutes)
                .UseSwagger(setup =>
                {
                    setup.RouteTemplate = "swagger/{documentName}/index.json";
                })
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/index.json", "Budget Tracker API V1");
                })
                .UseStatusCodePages();
        }

        private static void BuildRoutes(IRouteBuilder routeOptions)
        {
            routeOptions
                .MapRoute("swagger", "swagger/{*route}")
                .MapRoute("default", "{controller=Home}/{action=Index}");
        }
    }
}