using Microsoft.Extensions.Configuration;
using Sims.CheatEngine.Contracts;

namespace Sims.CheatEngine.Api
{
    public class ApplicationSettings : IApplicationSettings
    {
        public string CultureName { get; set; }
        public long? DistributedMemoryCacheSizeLimitInBytes { get; set; }

        public ApplicationSettings(IConfiguration configuration)
        {
            configuration.Bind(this);
        }
    }
}