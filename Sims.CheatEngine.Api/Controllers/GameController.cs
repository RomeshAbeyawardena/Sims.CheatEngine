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
            var query = requestViewModel.Query?.ToLower();
            return Ok(string.IsNullOrEmpty(query) 
                ? cheats 
                : cheats.Where(cheat => cheat.Name.ToLower().Contains(query) 
                                        || cheat.Description.ToLower().Contains(query) 
                                        || cheat.Code.ToLower().Contains(query)));
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