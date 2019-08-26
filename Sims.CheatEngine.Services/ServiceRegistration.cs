using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Sims.CheatEngine.Contracts.Providers;
using Sims.CheatEngine.Contracts.Services;
using Sims.CheatEngine.Domains.Data;
using WebToolkit.Common.Extensions;
using WebToolkit.Contracts;

namespace Sims.CheatEngine.Services
{
    public class ServiceRegistration : IServiceRegistration
    {
        public void RegisterServices(IServiceCollection services)
        {
            services
                .AddScoped<IWebCacheProvider, WebCacheProvider>()
                .AddScoped<ICheatService, CheatService>()
                .AddScoped<IGameService, GameService>()
                .RegisterDataPool<IEnumerable<Cheat>, int>()
                .AddDefaultValueProvider<Cheat>()
                .AddDefaultValueProvider<Game>();
        }
    }
}