using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using EfSamurai.Data;
using EfSamurai.Domain;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace EfSamurai.App
{
    class Program
    {
        static void Main(string[] args)
        {
            ClearDatabase();
            AddSomeSamurai();
            AddSomeBattles();
            AddOneSamuraiWithRelatedData();
            ListAllSamurai();
        }

        public static List<Samurai> ReadAllSamurai()
        {
            List<Samurai> samurai = null;

            using (var context = new SamuraiContext())
            {
                samurai = context.Samurais.ToList();
            }

            return samurai;
        }

        private static void ListAllSamurai()
        {
            List<Samurai> samurai = null;

            using (var context = new SamuraiContext())
            {
                samurai = context.Samurais.OrderBy(s => s.Name).ToList();
            }

            PrintSamurai(samurai);
        }

        private static void PrintSamurai(List<Samurai> samurai)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{"Samurai",-20}{"Hairstyle",-15}{"SecretIdentity",-20}{"Quotes",-30}");
            Console.ResetColor();
            Console.WriteLine();

            foreach (var s in samurai)
            {
                Console.Write($"{s.Name,-20}{s.Hairstyle.ToString(),-15}");

                //if (s.Quotes.Count > 0)
                //{
                //    foreach (var quote in s.Quotes)
                //    {
                //        Console.WriteLine($"{"",-55}{"'" + quote.Text + "'",-30}");
                //    }
                //}
                //else
                //{
                //    Console.WriteLine();
                //}


                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private static void AddOneSamuraiWithRelatedData()
        {
            var samurai = new Samurai
            {
                Name = "Obi-Wan Kenobi",
                Quotes = new List<Quote>
                {
                    new Quote
                    {
                        Text = "Well, hello there.",
                        Type = QuoteType.Cheesy
                    },
                    new Quote
                    {
                        Text = "So uncivilzed!",
                        Type = QuoteType.Awesome
                    },
                    new Quote
                    {
                        Text = "I have a bad feeling about this.",
                        Type = QuoteType.Lame
                    }
                },
                Hairstyle = Hairstyle.Western,
                SecretIdentity = new SecretIdentity {Name = "Ben Kenobi"}
            };
            samurai.AddBattle(new Battle
            {
                Name = "Battle of Utapau",
                Description = "The final battle of the war against the separatists.",
                Brutal = true,
                StartDate = new DateTime(6212, 05, 15),
                EndDate = new DateTime(6212, 05, 15),
                BattleLog = new BattleLog
                {
                    Name = "Chronicle of the Battle of Utapau",
                    Events = new List<BattleEvent>
                    {
                        new BattleEvent
                        {
                            Time = new DateTime(6212, 05, 15),
                            Summary = "Lord Kenobi greets General Grievous",
                            Description = "Well, hello there, lord Kenobi greeted the general..."
                        },
                        new BattleEvent
                        {
                            Time = new DateTime(6212, 05, 15),
                            Summary = "The Betrayal",
                            Description =
                                "It was at that moment, that the mercenaries recieved the order from the Emperor, and they turned their weapons upon the samurai."
                        }
                    }

                }
            });

            using (var context = new SamuraiContext())
            {
                context.Samurais.Add(samurai);
                context.SaveChanges();
            }

        }


        private static void AddSomeSamurai()
        {
            var samurai = new List<Samurai>
            {
                new Samurai
                {
                    Name = "Akira",
                    Quotes = new List<Quote>
                    {
                        new Quote
                        {
                            Text = "To hell and back",
                            Type = QuoteType.Cheesy
                        }
                    },
                    Hairstyle = Hairstyle.Chonmage,
                    SecretIdentity = new SecretIdentity {Name = "Nyponrosen"}
                },
                new Samurai
                {
                    Name = "Miyamoto Musahi",
                    Quotes = new List<Quote>
                    {
                        new Quote
                        {
                            Text = "Face me!",
                            Type = QuoteType.Cheesy
                        }

                    },
                    Hairstyle = Hairstyle.Chonmage
                },
                new Samurai
                {
                    Name = "Yamamoto",
                    Hairstyle = Hairstyle.Western
                }
            };

            using (var context = new SamuraiContext())
            {
                context.Samurais.AddRange(samurai);
                context.SaveChanges();
            }
        }

        private static void AddSomeBattles()
        {
            var battles = new List<Battle>();
            battles.Add(new Battle
            {
                Name = "Battle of Apple Blossom Valley",
                Description = "It lasted for ten days and ten nights.",
                Brutal = false,
                StartDate = new DateTime(1299, 04, 01),
                EndDate = new DateTime(1299, 04, 11),
                BattleLog = new BattleLog
                {
                    Name = "Chronicle of the Battle of Apple Blossom Valley",
                    Events = new List<BattleEvent>
                    {
                        new BattleEvent
                        {
                            Time = new DateTime(1299,04,07),
                            Summary = "Charge of the Lost Clans",
                            Description = "The Clans had been lost for some time, and now they charged..."
                        },
                        new BattleEvent
                        {
                            Time = new DateTime(1299,04,11),
                            Summary = "The Final Night",
                            Description = "It was a moonless night, and the samurai had fought without rest for ten days and ten nights..."
                        }
                    }

                }
            });

            using (var context = new SamuraiContext())
            {
                context.Battles.AddRange(battles);
                context.SaveChanges();
            }
        }

        private static void ClearDatabase()
        {
            using (var context = new SamuraiContext())
            {
                context.Samurais.RemoveRange(context.Samurais);
                context.Battles.RemoveRange(context.Battles);
                context.SaveChanges();
            }
        }

    }
}
