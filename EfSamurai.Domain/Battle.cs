using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace EfSamurai.Domain
{
    public class Battle
    {
        //Ett krig ska kunna ha olika värden: namn, beskrivning, brutal(true/false), start-datum, slut-datum

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Brutal { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }      
        public BattleLog BattleLog { get; set; }
        public virtual ICollection<SamuraiInBattle> Participants { get; set; }

    }

    public class BattleLog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BattleEvent> Events { get; set; }

    }

    public class BattleEvent
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public DateTime Time { get; set; }
    }


}
