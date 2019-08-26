using System.Collections.Generic;
using System.Threading.Tasks;
using Sims.CheatEngine.Domains.Data;

namespace Sims.CheatEngine.Contracts.Services
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetGames();
        Task<Game> SaveGame(Game game, bool saveChanges = true);
    }
}