using System.ComponentModel.DataAnnotations;

namespace Sims.CheatEngine.Domains.ViewModels
{
    public class SaveGameViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}