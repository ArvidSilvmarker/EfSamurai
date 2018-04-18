using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using EfSamurai.Data;
using EfSamurai.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace EfSamurai.App
{
    class Program
    {
        private static SamuraiContext _context;

        static void Main(string[] args)
        {
            //_context = new SamuraiContext();
            //_context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());

            //Initialize(new SamuraiContext());
            //ClearDatabase();
            //ReseedAllTables();
            //AddSomeSamurai();
            //AddSomeBattles();
            //AddOneSamuraiWithRelatedData();
            //ListAllSamurai();
            //PrintSamuraiwithSecretName("Galnakossan");
            //ListAllQuotesOfType(QuoteType.Cheesy);
            //PrintBattles(ListAllBattles(new DateTime(800, 1, 1), new DateTime(1900, 1, 1),null));
            //List<string> namesWithAlias = AllSamuariNameWithAlias();
            //DisplayList(namesWithAlias);
            //PrintBattlesWithLog(ListAllBattles(new DateTime(800, 1, 1), new DateTime(1900, 1, 1), null));
            PrintSamuraiInfo(ReadSamuraiInfo());
            //GetBattlesForSamurai("Obi-Wan Kenobi");
            //LoggingLab();
        }

        private static void PrintSamuraiInfo(List<SamuraiInfo> info)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{"Name", -25}{"Secret Identity",-20}{"Battles",-80}");
            Console.ResetColor();
            foreach (var samuraiInfo in info)
            {
                Console.WriteLine($"{samuraiInfo.Name,-25}{samuraiInfo.RealName,-20}{samuraiInfo.BattleNames,-80}");
            }
        }

        private static void DisplayList(List<string> text)
        {
            Console.WriteLine();
            text.ForEach(Console.WriteLine);
            Console.WriteLine();
        }

        private static void PrintBattles(List<Battle> battles)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{"Name",-40}{"From",-20}{"To",-20}{"Brutal",-10}");
            Console.ResetColor();
            foreach (var battle in battles)
            {
                Console.WriteLine($"{battle.Name,-40}{GetDate(battle.StartDate),-20}{GetDate(battle.EndDate),-20}{(battle.Brutal ? "yes" : "no")}");
            }
        }

        private static void PrintBattlesWithLog(List<Battle> battles)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("The Grand List of Battles");
            Console.ResetColor();
            foreach (var battle in battles)
            {
                Console.WriteLine($"{battle.Name}");
                Console.WriteLine($"It was a {(battle.Brutal ? "brutal" : "heroic")} battle from {GetDate(battle.StartDate)} to {GetDate(battle.EndDate)}.");
                Console.WriteLine($"The battle was chronicled in {battle.BattleLog.Name}:");
                foreach (var battleEvent in battle.BattleLog.Events.OrderBy(e => e.Time).ToList())
                {
                    Console.WriteLine();
                    Console.WriteLine($"At {GetDate(battleEvent.Time)} - {battleEvent.Summary}");
                    Console.WriteLine($"{battleEvent.Description}");
                }

                Console.WriteLine("_______________________________________________________________________________");
            }

            
        }

        private static string GetDate(DateTime dt)
        {
            CultureInfo ci = new CultureInfo("ja-JP");
            return dt.ToString("D", ci);
        }

        private static List<Battle> ListAllBattles(DateTime from, DateTime to, bool? isBrutal)
        {
            List<Battle> battles = null;
            using (var context = new SamuraiContext())
            {
                var battlesWithLogAndEvents = context.Battles.Select(b => b.BattleLog).Select(l => l.Events).ToList();
                battles = context.Battles
                    .Where(battle => battle.StartDate >= from)
                    .Where(battle => battle.EndDate <= to).ToList();
                if (isBrutal != null)
                    battles = battles.Where(battle => battle.Brutal == isBrutal).ToList();
            }

            return battles;
        }

        private static void ListAllQuotesOfType(QuoteType quoteType)
        {
            Console.WriteLine();
            List<Quote> quotes = new List<Quote>();
            using (var context = new SamuraiContext())
            {
                context.Samurais.SelectMany(s => s.Quotes).Where(q => q.Type == quoteType).ToList().ForEach(q => quotes.Add(q));
            }

            if (quotes.Count == 0)
                Console.WriteLine($"There are no quotes of type {quoteType.ToString()}.");
            else
                foreach (var quote in quotes)
                    Console.WriteLine($"\"{quote.Text}\" is a {quote.Type.ToString().ToLower()} quote said by {FindSamurai(quote.SamuraiId).Name}.");

            Console.WriteLine();
        }

        private static Samurai FindSamurai(int id)
        {
            using (var context = new SamuraiContext())
            {
                return context.Samurais.SingleOrDefault(s => s.Id == id);
            }
        }

        private static void PrintSamuraiwithSecretName(string secretName)
        {
            Samurai secretSamurai = FindSamuraiWithSecretName(secretName);
            if (secretSamurai == null)
                Console.WriteLine($"There is no Samurai known by the name of {secretName}.");
            else
                PrintSamurai(new List<Samurai>{secretSamurai});
        }


        private static Samurai FindSamuraiWithSecretName(string secretName)
        {
            Samurai secretSamurai = null;
            using (var context = new SamuraiContext())
            {
                secretSamurai = context.Samurais.SingleOrDefault(s => s.SecretIdentity.Name == secretName);
            }

            return secretSamurai;
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
                StartDate = new DateTime(1545, 05, 15),
                EndDate = new DateTime(1545, 05, 15),
                BattleLog = new BattleLog
                {
                    Name = "Chronicle of the Battle of Utapau",
                    Events = new List<BattleEvent>
                    {
                        new BattleEvent
                        {
                            Time = new DateTime(1545, 05, 15),
                            Summary = "Lord Kenobi greets General Grievous",
                            Description = "Well, hello there, lord Kenobi greeted the general..."
                        },
                        new BattleEvent
                        {
                            Time = new DateTime(1545, 05, 15),
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
                    Hairstyle = Hairstyle.Oicho
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

        private static List<string> AllSamuariNameWithAlias()
        {
            List<string> text = new List<string>();
            using (var context = new SamuraiContext())
            {
                context.Samurais.Select(s => s.SecretIdentity).ToList();
               
                foreach (Samurai samurai in context.Samurais.ToList())
                {
                    if (samurai.SecretIdentity != null)
                        text.Add($"{samurai.Name} alias {samurai.SecretIdentity.Name}");
                }
                
            }

            return text;
        }

        private static List<SamuraiInfo> ReadSamuraiInfo()
        {
            List<SamuraiInfo> infos = new List<SamuraiInfo>();
            using (var context = new SamuraiContext())
            {
                var list = context.Samurais
                    .Include(s => s.SecretIdentity)
                    .Include(s => s.Battles)
                    .ThenInclude(s => s.Battle).ToList();


                foreach (var samurai in list)
                {
                    string battles = "";
                    foreach (var samuraiBattle in samurai.Battles.ToList())
                    {
                        battles += samuraiBattle.Battle.Name + ",";
                    }

                    if (battles.Length > 0)
                        battles.Remove(battles.Length - 1);

                    infos.Add(new SamuraiInfo
                    {
                        Name = samurai.Name ?? "",
                        BattleNames = String.Join(",",samurai.Battles.Select(s => s.Battle.Name)),
                        RealName = samurai.SecretIdentity?.Name
                    });
                }

                infos.Add(new SamuraiInfo());

            }

            return infos;
        }

        private static void GetBattlesForSamurai(string samuraiName)
        {
            List<Battle> battles = new List<Battle>();
            using (var context = new SamuraiContext())
            {
                var samurai = context.Samurais.Include(s => s.Battles).ThenInclude(b => b.Battle).FirstOrDefault(s => s.Name == samuraiName);

                if (samurai != null)
                {
                    Console.WriteLine($"Samurai {samuraiName} is a veteran of the following battles: ");
                    foreach (var samuraiInBattle in samurai.Battles)
                    {
                        Console.WriteLine($"ID: {samuraiInBattle.BattleId}    Name: {samuraiInBattle.Battle.Name}");
                    }
                } 
            }
        }

        private static void LoggingLab()
        {
            using (var context = new SamuraiContext())
            {
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("var query = context.Samurais;");
                Console.WriteLine("----------------------------------------------");
                var query = context.Samurais;
                Console.ReadKey();


                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("var samurais = context.Samurais.ToList();");
                Console.WriteLine("----------------------------------------------");
                var samurais = context.Samurais.ToList();
                Console.ReadKey();

                Console.WriteLine("----------------------------------------------");
                Console.WriteLine(" foreach (var samurai in samurais) {Console.WriteLine(samurai.Name);}");
                Console.WriteLine("----------------------------------------------");
                foreach (var samurai in samurais)
                {
                    Console.WriteLine(samurai.Name);
                }
                Console.ReadKey();

                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("var first = context.Samurais.First();");
                Console.WriteLine("----------------------------------------------");
                var first = context.Samurais.First();
                Console.ReadKey();

                // Lägg till samurai i context utan att anropa SaveChanges
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("context.Samurais.Add(new Samurai { Name = \"Sven\" });");
                Console.WriteLine("----------------------------------------------");
                context.Samurais.Add(new Samurai { Name = "Sven" });
                Console.ReadKey();

                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("context.SaveChanges();");
                Console.WriteLine("----------------------------------------------");
                context.SaveChanges();
                Console.ReadKey();
            }

        }

        private static void ReseedAllTables()
        {
            using (var context = new SamuraiContext())
            {
                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Samurais', RESEED, 0)");
                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('SecretIdentity', RESEED, 0)");
                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Quote', RESEED, 0)");
                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Battles', RESEED, 0)");
                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('BattleLog', RESEED, 0)");
                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('BattleEvent', RESEED, 0)");
            }

        }

        public static void Initialize(SamuraiContext context)

        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
