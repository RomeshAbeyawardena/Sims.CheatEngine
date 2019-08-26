using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sims.CheatEngine.Contracts.Services;
using Sims.CheatEngine.Domains.Data;
using Sims.CheatEngine.Domains.ViewModels;

namespace Sims.CheatEngine.Api.Controllers
{
    public class CheatController : ControllerBase
    {
        private readonly ICheatService _cheatService;

        public CheatController(ICheatService cheatService)
        {
            _cheatService = cheatService;
        }

        [HttpPost]
        public async Task<ActionResult> SaveCheat([FromBody] SaveCheatViewModel saveCheatViewModel)
        {
            var cheat = Map<SaveCheatViewModel, Cheat>(saveCheatViewModel);
            return Ok(await _cheatService.SaveCheat(cheat));
        }
    }
}