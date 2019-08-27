using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Sims.CheatEngine.Contracts.Providers;
using Sims.CheatEngine.Contracts.Services;
using Sims.CheatEngine.Domains.Data;
using WebToolkit.Common.Extensions;
using WebToolkit.Contracts;
using WebToolkit.Contracts.Providers;

namespace Sims.CheatEngine.Services
{
    public class ServiceRegistration : IServiceRegistration
    {
        public void RegisterServices(IServiceCollection services)
        {
            var dateTimeProvider = services.GetRequiredService<IDateTimeProvider>();

            services
                .AddScoped<IWebCacheProvider, WebCacheProvider>()
                .AddScoped<ICheatService, CheatService>()
                .AddScoped<IGameService, GameService>()
                .RegisterDataPool<IEnumerable<Cheat>, int>()
                .AddDefaultValueProvider<Cheat>(cheat =>
                {
                    cheat.Created = dateTimeProvider.Now;
                    cheat.Modified = dateTimeProvider.Now;
                })
                .AddDefaultValueProvider<Game>(game =>
                {
                    game.Created = DateTimeOffset.Now;
                    game.Modified = DateTimeOffset.Now;
                });
        }
    }
}