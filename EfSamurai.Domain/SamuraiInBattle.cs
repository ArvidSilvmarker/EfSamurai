using System;
using System.Collections.Generic;
using System.Text;

namespace EfSamurai.Domain
{
    public class SamuraiInBattle
    {
        public int SamuraiId { get; set; }
        public int BattleId { get; set; }
        public Samurai Samurai { get; set; }
        public Battle Battle { get; set; }

    }
}
