using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sims.CheatEngine.Contracts.Services;
using Sims.CheatEngine.Domains.Data;
using Sims.CheatEngine.Domains.ViewModels;
using WebToolkit.Contracts;

namespace Sims.CheatEngine.Api.Controllers
{
    public class CheatController : ControllerBase
    {
        private readonly ICheatService _cheatService;
        private readonly IDataPool<IEnumerable<Cheat>,int> _dataPoolFactory;

        public CheatController(ICheatService cheatService, IDataPool<IEnumerable<Cheat>,int> dataPoolFactory)
        {
            _cheatService = cheatService;
            _dataPoolFactory = dataPoolFactory;
        }

        [HttpPost]
        public async Task<ActionResult> SaveCheat([FromBody] SaveCheatViewModel saveCheatViewModel)
        {
            var cheat = Map<SaveCheatViewModel, Cheat>(saveCheatViewModel);
            var savedCheat = await _cheatService.SaveCheat(cheat);

            var value = _dataPoolFactory.Retrieve(saveCheatViewModel.GameId);
            if (value != null)
                _dataPoolFactory.Add(savedCheat.GameId, value.Append(savedCheat));
            
            return Ok(savedCheat);
        }
    }
}