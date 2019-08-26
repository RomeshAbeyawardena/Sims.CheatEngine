using System.ComponentModel.DataAnnotations;

namespace Sims.CheatEngine.Domains.ViewModels
{
    public class SaveCheatViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Code { get; set; }
        public int GameId { get; set; }
    }
}