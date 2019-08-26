using System.Collections.Generic;
using System.Threading.Tasks;
using Sims.CheatEngine.Domains.Data;

namespace Sims.CheatEngine.Contracts.Providers
{
    public interface IWebCacheProvider
    {
        Task<IEnumerable<Game>> Games { get; }
        Task<IEnumerable<Cheat>> GetCheats(int gameId);
        Task ClearCache(string key);
    }
}