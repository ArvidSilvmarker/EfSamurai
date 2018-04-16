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


    }

    public enum Hairstyle
    {
        Chonmage,
        Oicho,
        Western
    }
}
