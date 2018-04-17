using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Text;

namespace EfSamurai.Domain
{
    public class Battle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Brutal { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Required]
        public BattleLog BattleLog { get; set; } 
        public ICollection<SamuraiInBattle> Participants { get; set; }

    }

    public class BattleLog
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        public List<BattleEvent> Events { get; set; }

        public int BattleId { get; set; }
        public Battle Battle { get; set; }

    }

    public class BattleEvent
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public DateTime Time { get; set; }
        [Required]
        public BattleLog BattleLog { get; set; }
    }


}
