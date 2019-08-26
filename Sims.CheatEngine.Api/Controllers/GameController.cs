using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sims.CheatEngine.Contracts.Providers;
using Sims.CheatEngine.Contracts.Services;
using Sims.CheatEngine.Domains.Data;
using Sims.CheatEngine.Domains.ViewModels;

namespace Sims.CheatEngine.Api.Controllers
{
    public class GameController : ControllerBase
    {
        private readonly IWebCacheProvider _webCacheProvider;
        private readonly IGameService _gameService;

        [HttpGet]
        public async Task<ActionResult> GetGames()
        {
            return Ok(await _webCacheProvider.Games);
        }

        [HttpGet]
        public async Task<ActionResult> GetCheats([FromQuery] GetCheatsRequestViewModel requestViewModel)
        {
            var cheats = await _webCacheProvider.GetCheats(requestViewModel.GameId);

            return Ok(string.IsNullOrEmpty(requestViewModel.Query) 
                ? cheats 
                : cheats.Where(cheat => cheat.Name.Contains(requestViewModel.Query) 
                                        || cheat.Code.Contains(requestViewModel.Query)));
        }

        [HttpPost]
        public async Task<ActionResult> SaveGame([FromBody] SaveGameViewModel saveGameViewModel)
        {
            var savedGame = await _gameService.SaveGame(
                Map<SaveGameViewModel, Game>(saveGameViewModel));

            await _webCacheProvider.ClearCache(nameof(_webCacheProvider.Games));

            return Ok(savedGame);
        }

        public GameController(IWebCacheProvider webCacheProvider,
            IGameService gameService)
        {
            _webCacheProvider = webCacheProvider;
            _gameService = gameService;
        }
    }
}