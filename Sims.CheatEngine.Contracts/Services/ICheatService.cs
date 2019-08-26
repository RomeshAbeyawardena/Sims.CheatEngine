using System.Collections.Generic;
using System.Threading.Tasks;
using Sims.CheatEngine.Domains.Data;

namespace Sims.CheatEngine.Contracts.Services
{
    public interface ICheatService
    {
        Task<IEnumerable<Cheat>> GetCheats(int gameId);
        Task<Cheat> SaveCheat(Cheat cheat, bool saveChanges = true);
    }
}