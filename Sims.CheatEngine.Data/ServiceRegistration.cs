using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sims.CheatEngine.Contracts;
using Sims.CheatEngine.Domains.Data;
using WebToolkit.Common.Extensions;
using WebToolkit.Contracts;

namespace Sims.CheatEngine.Data
{
    public class ServiceRegistration : IServiceRegistration
    {
        public void RegisterServices(IServiceCollection services)
        {
            var connectionStrings = services.GetRequiredService<IConnectionStrings>();

            services
                .AddDbContext<CheatEngineDbContext>(options => options.UseSqlServer(connectionStrings.Default))
                .RegisterRepositories<CheatEngineDbContext>(typeof(Cheat), typeof(Game));
        }
    }
}