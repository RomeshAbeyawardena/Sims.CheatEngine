using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sims.CheatEngine.Contracts.Services;
using Sims.CheatEngine.Domains.Data;
using WebToolkit.Contracts.Data;

namespace Sims.CheatEngine.Services
{
    public class CheatService : ICheatService
    {
        private readonly IRepository<Cheat> _cheatRepository;

        public async Task<IEnumerable<Cheat>> GetCheats(int gameId)
        {
            return await _cheatRepository
                .Query(cheat => cheat.GameId == gameId)
                .ToArrayAsync();
        }

        public async Task<Cheat> SaveCheat(Cheat cheat, bool saveChanges = true)
        {
            if (cheat.Id > 0)
                cheat = _cheatRepository.Attach(cheat);

            return await _cheatRepository.SaveChangesAsync(cheat, saveChanges);
        }

        public CheatService(IRepository<Cheat> cheatRepository)
        {
            _cheatRepository = cheatRepository;
        }
    }
}