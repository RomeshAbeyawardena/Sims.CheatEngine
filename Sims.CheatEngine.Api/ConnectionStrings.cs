using Microsoft.Extensions.Configuration;
using Sims.CheatEngine.Contracts;

namespace Sims.CheatEngine.Api
{
    public class ConnectionStrings : IConnectionStrings
    {
        public ConnectionStrings(IConfiguration configuration)
        {
            configuration
                .GetSection("connectionStrings")
                .Bind(this);
        }

        public string Default { get; set; }
    }
}