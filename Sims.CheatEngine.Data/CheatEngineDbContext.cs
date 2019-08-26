using Microsoft.EntityFrameworkCore;
using Sims.CheatEngine.Domains.Data;
using WebToolkit.Common;
using WebToolkit.Contracts.Providers;

namespace Sims.CheatEngine.Data
{
    public class CheatEngineDbContext : ExtendedDbContext
    {
        public CheatEngineDbContext(DbContextOptions options, IDateTimeProvider dateTimeProvider) 
            : base(options, Options<DbContextExtendedOptions>.Create(opt => opt.SingulariseTableNames = true),
                dateTimeProvider)
        {
            
        }

        public virtual DbSet<Cheat> Cheats { get; set; }
        public virtual DbSet<Game> Games { get; set; }
    }
}