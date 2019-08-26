using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sims.CheatEngine.Contracts.Services;
using Sims.CheatEngine.Domains.Data;
using WebToolkit.Contracts.Data;

namespace Sims.CheatEngine.Services
{
    public class GameService : IGameService
    {
        private readonly IRepository<Game> _gameRepository;

        public async Task<IEnumerable<Game>> GetGames()
        {
            return await _gameRepository.Query().ToArrayAsync();
        }

        public async Task<Game> SaveGame(Game game, bool saveChanges = true)
        {
            return await _gameRepository.SaveChangesAsync(game, saveChanges);
        }

        public GameService(IRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }
    }
}