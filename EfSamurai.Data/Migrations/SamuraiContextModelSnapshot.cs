﻿// <auto-generated />
using EfSamurai.Data;
using EfSamurai.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace EfSamurai.Data.Migrations
{
    [DbContext(typeof(SamuraiContext))]
    partial class SamuraiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EfSamurai.Domain.Battle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BattleLogId");

                    b.Property<bool>("Brutal");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Name");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("BattleLogId");

                    b.ToTable("Battles");
                });

            modelBuilder.Entity("EfSamurai.Domain.BattleEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BattleLogId");

                    b.Property<string>("Description");

                    b.Property<string>("Summary");

                    b.Property<DateTime>("Time");

                    b.HasKey("Id");

                    b.HasIndex("BattleLogId");

                    b.ToTable("BattleEvent");
                });

            modelBuilder.Entity("EfSamurai.Domain.BattleLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("BattleLog");
                });

            modelBuilder.Entity("EfSamurai.Domain.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("SamuraiId");

                    b.Property<string>("Text");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("SamuraiId");

                    b.ToTable("Quote");
                });

            modelBuilder.Entity("EfSamurai.Domain.Samurai", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Hairstyle");

                    b.Property<string>("Name");

                    b.Property<int?>("SecretIdentityId");

                    b.HasKey("Id");

                    b.HasIndex("SecretIdentityId");

                    b.ToTable("Samurais");
                });

            modelBuilder.Entity("EfSamurai.Domain.SamuraiInBattle", b =>
                {
                    b.Property<int>("SamuraiId");

                    b.Property<int>("BattleId");

                    b.HasKey("SamuraiId", "BattleId");

                    b.HasIndex("BattleId");

                    b.ToTable("SamuraiInBattle");
                });

            modelBuilder.Entity("EfSamurai.Domain.SecretIdentity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("SecretIdentity");
                });

            modelBuilder.Entity("EfSamurai.Domain.Battle", b =>
                {
                    b.HasOne("EfSamurai.Domain.BattleLog", "BattleLog")
                        .WithMany()
                        .HasForeignKey("BattleLogId");
                });

            modelBuilder.Entity("EfSamurai.Domain.BattleEvent", b =>
                {
                    b.HasOne("EfSamurai.Domain.BattleLog")
                        .WithMany("Events")
                        .HasForeignKey("BattleLogId");
                });

            modelBuilder.Entity("EfSamurai.Domain.Quote", b =>
                {
                    b.HasOne("EfSamurai.Domain.Samurai")
                        .WithMany("Quotes")
                        .HasForeignKey("SamuraiId");
                });

            modelBuilder.Entity("EfSamurai.Domain.Samurai", b =>
                {
                    b.HasOne("EfSamurai.Domain.SecretIdentity", "SecretIdentity")
                        .WithMany()
                        .HasForeignKey("SecretIdentityId");
                });

            modelBuilder.Entity("EfSamurai.Domain.SamuraiInBattle", b =>
                {
                    b.HasOne("EfSamurai.Domain.Battle", "Battle")
                        .WithMany("Participants")
                        .HasForeignKey("BattleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EfSamurai.Domain.Samurai", "Samurai")
                        .WithMany("Battles")
                        .HasForeignKey("SamuraiId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
