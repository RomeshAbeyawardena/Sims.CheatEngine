using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
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
            var mappedCheat = Map<SaveCheatViewModel, Cheat>(saveCheatViewModel);
            var savedCheat = await _cheatService.SaveCheat(mappedCheat);

            var value = _dataPoolFactory.Retrieve(saveCheatViewModel.GameId)?.ToArray();
            if (value != null)
            {
                if(saveCheatViewModel.Id == 0)
                    _dataPoolFactory.Add(savedCheat.GameId, value.Append(savedCheat));
                else
                {
                    var item = value.FirstOrDefault(cheat => cheat.Id == saveCheatViewModel.Id);
                    if(item != null)
                        value[value.IndexOf(item)] = savedCheat;

                    _dataPoolFactory.Add(savedCheat.GameId, value);
                }
            }

            return Ok(savedCheat);
        }
    }
}