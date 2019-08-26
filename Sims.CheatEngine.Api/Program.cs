using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Sims.CheatEngine.Api
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await WebHostBuilder(args)
                .UseKestrel()
                .Build()
                .RunAsync();
        }

        public static IWebHostBuilder WebHostBuilder(string[] args) => WebHost.CreateDefaultBuilder<Startup>(args);
    }
}