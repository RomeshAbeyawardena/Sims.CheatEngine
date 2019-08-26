using System.Collections.Generic;
using Sims.CheatEngine.Domains.Data;

namespace Sims.CheatEngine.Domains.ViewModels.Response
{
    public class GetGameResponseViewModel
    {
        public Game Game { get; set; }
        public IEnumerable<Cheat> Cheats { get; set; }
    }
}