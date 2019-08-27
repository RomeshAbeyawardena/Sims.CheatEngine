using System;
using System.ComponentModel.DataAnnotations;
using WebToolkit.Contracts;

namespace Sims.CheatEngine.Domains.Data
{
    public class Cheat : ICreated, IModified
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int GameId { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Modified { get; set; }
    }
}