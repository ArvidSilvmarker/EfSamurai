using System;
using System.Collections.Generic;
using System.Text;

namespace EfSamurai.Domain
{
    public class Samurai
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Quote> Quotes { get; set; }
        public Hairstyle Hairstyle { get; set; }
        public SecretIdentity SecretIdentity { get; set; }
        public ICollection<SamuraiInBattle> Battles { get; set; }

        public Samurai(string name)
        {
            Name = name;
        }

        public Samurai()
        {
            
        }

        public void AddBattle(Battle battle)
        {
            if (Battles == null)
                Battles = new List<SamuraiInBattle>();
            Battles.Add(new SamuraiInBattle{Samurai = this, Battle = battle});
        }

    }

    public enum Hairstyle
    {
        Chonmage,
        Oicho,
        Western
    }
}
