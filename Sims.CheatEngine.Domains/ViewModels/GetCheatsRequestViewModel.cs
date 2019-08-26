using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Sims.CheatEngine.Domains.ViewModels
{
    public class GetCheatsRequestViewModel
    {
        [Required]
        public int GameId { get; set; }
        [FromQuery(Name = "q")]
        public string Query { get; set; }
    }
}