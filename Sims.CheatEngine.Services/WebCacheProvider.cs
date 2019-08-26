using System.Collections.Generic;
using System.Threading.Tasks;
using Sims.CheatEngine.Contracts.Providers;
using Sims.CheatEngine.Contracts.Services;
using Sims.CheatEngine.Domains.Data;
using WebToolkit.Common.Extensions;
using WebToolkit.Contracts.Factories;
using WebToolkit.Contracts.Providers;

namespace Sims.CheatEngine.Services
{
    public class WebCacheProvider : IWebCacheProvider
    {
        private readonly IDataPoolFactory _dataPoolFactory;
        private readonly ICacheProvider _cacheProvider;
        private readonly IGameService _gameService;
        private readonly ICheatService _cheatService;

        public Task<IEnumerable<Game>> Games => _cacheProvider.LoadAsync(CacheType.DistributedCache,
            nameof(Games), async() => await _gameService.GetGames());

        public async Task<IEnumerable<Cheat>> GetCheats(int gameId)
        {
            return await 
                _dataPoolFactory.GetValueAsync(gameId, async(a) => await _cheatService.GetCheats(a));
        }

        public WebCacheProvider(IDataPoolFactory dataPoolFactory, ICacheProvider cacheProvider, 
            IGameService gameService, ICheatService cheatService)
        {
            _dataPoolFactory = dataPoolFactory;
            _cacheProvider = cacheProvider;
            _gameService = gameService;
            _cheatService = cheatService;
        }
    }
}