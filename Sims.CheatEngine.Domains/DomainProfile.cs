using AutoMapper;
using Sims.CheatEngine.Domains.Data;
using Sims.CheatEngine.Domains.ViewModels;

namespace Sims.CheatEngine.Domains
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<SaveGameViewModel, Game>();
            CreateMap<SaveCheatViewModel, Cheat>();
        }
    }
}