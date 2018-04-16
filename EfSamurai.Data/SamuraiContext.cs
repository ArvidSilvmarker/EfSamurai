using System;
using System.Collections.Generic;
using System.Text;
using EfSamurai.Domain;
using Microsoft.EntityFrameworkCore;

namespace EfSamurai.Data
{
    public class SamuraiContext : DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server = (localdb)\\mssqllocaldb; Database = EfSamurai; Trusted_Connection = True; ");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiInBattle>()
                .HasKey(sb => new { sb.SamuraiId, sb.BattleId });
        }
    }
}
